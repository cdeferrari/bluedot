using ApiCore.Services.Contracts.AccountStatuss;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.ConsortiumConfigurations;
using ApiCore.Services.Contracts.UnitConfigurations;
using ApiCore.Services.Contracts.Consortiums;
using ApiCore.Services.Contracts.Unit;
using ApiCore.Dtos.Response;

namespace ApiCore.Services.Implementations.AccountStatuss
{
    public class AccountStatusService : IAccountStatusService
    {
        private static readonly decimal INTEREST = (decimal)0.02;

        public IPaymentRepository PaymentRepository { get; set; }
        public IPatrimonyStatusRepository PatrimonyStatusRepository { get; set; }
        public IPaymentTypeRepository PaymentTypeRepository { get; set; }
        public IAccountStatusRepository AccountStatusRepository { get; set; }        
        public IFunctionalUnitRepository UnitRepository { get; set; }
        public ISpendRepository SpendRepository { get; set; }
        public IIncomeRepository IncomeRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }
        public ISpendTypeRepository SpendTypeRepository { get; set; }
        public IConsortiumConfigurationRepository ConsortiumConfigurationRepository { get; set; }
        public IUnitConfigurationRepository UnitConfigurationRepository { get; set; }
        public IUnitConfigurationTypeRepository UnitConfigurationTypeRepository { get; set; }
        public IConsortiumConfigurationTypeRepository ConsortiumConfigurationTypeRepository { get; set; }
        public IConsortiumService ConsortiumService { get; set; }
        public IUnitService UnitService { get; set; }
        public IConsortiumConfigurationsService ConsortiumConfigurationService { get; set; }
        public IUnitConfigurationsService UnitConfigurationService { get; set; }

        [Transaction]
        public AccountStatus CreateAccountStatus(AccountStatusRequest AccountStatus)
        {        
            var entityToInsert = new AccountStatus() { };
            MergeAccountStatus(entityToInsert, AccountStatus);

            if (entityToInsert.IsPayment())
            {

                var unit = UnitRepository.GetById(AccountStatus.UnitId);
                var consortium = ConsortiumRepository.GetAll().Where(x => x.Ownership.Id == unit.Ownership.Id).FirstOrDefault();
                
                var dateDay = AccountStatus.StatusDate.Day;
                var limitDateConfiguration = ConsortiumConfigurationRepository
                .GetAll().Where(x => x.Consortium.Id == consortium.Id && x.Type.Description == "Día límite pago adelantado")
                .OrderByDescending(x => x.ConfigurationDate)
                        .FirstOrDefault();
                
                var limitDayValue = limitDateConfiguration != null ? Convert.ToInt32(limitDateConfiguration.Value): 0;
                if(dateDay <= limitDayValue)
                {

                    var advancedPaymentConfiguration = ConsortiumConfigurationRepository
                .GetAll().Where(x => x.Consortium.Id == consortium.Id && x.Type.Description == "Descuento por pago adelantado")
                .OrderByDescending(x => x.ConfigurationDate)
                        .FirstOrDefault();

                        
                    var advandedPaymentValue = advancedPaymentConfiguration != null ? advancedPaymentConfiguration.Value : 0;

                    this.RecalculateAmount(entityToInsert, advandedPaymentValue);
                    
                }

            }
            
            AccountStatusRepository.Insert(entityToInsert);

            if (entityToInsert.IsPayment())
            {
                
                var payment = new Payment()
                {
                    AccountStatus = entityToInsert,
                    PaymentType = PaymentTypeRepository.GetById(AccountStatus.PaymentTypeId.Value)
                };

                PaymentRepository.Insert(payment);

                var unit = UnitRepository.GetById(AccountStatus.UnitId);
                var consortium = ConsortiumRepository.GetAll().Where(x => x.Ownership.Id == unit.Ownership.Id).FirstOrDefault();

                var actualPatrimonyStatus = this.PatrimonyStatusRepository.GetByConsortiumId(consortium.Id)
                    .OrderByDescending(x => x.StatusDate).FirstOrDefault();


                var patrimonyStatus = new PatrimonyStatus()
                {
                    Activo = actualPatrimonyStatus != null ? actualPatrimonyStatus.Activo + entityToInsert.Haber : entityToInsert.Haber,
                    Debe = 0,
                    Pasivo = actualPatrimonyStatus != null ? actualPatrimonyStatus.Pasivo - entityToInsert.Haber : - entityToInsert.Haber,
                    Haber  = entityToInsert.Haber,
                    Consortium = consortium,
                    StatusDate = DateTime.Now                    
                };

                this.PatrimonyStatusRepository.Insert(patrimonyStatus);
            }

            
            return entityToInsert;
        }

        private void RecalculateAmount(AccountStatus status, decimal advandedPaymentValue)
        {
            var paymentPercentage = 100 - advandedPaymentValue;
            var total = 100 * status.Haber / paymentPercentage;
            status.Haber = total;

        }

        public AccountStatus GetById(int AccountStatusId)
        {
            var AccountStatus = AccountStatusRepository.GetById(AccountStatusId);
            if (AccountStatus == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return AccountStatus;
        }
        

        [Transaction]
        public AccountStatus UpdateAccountStatus(AccountStatus originalAccountStatus, AccountStatusRequest AccountStatus)
        {            
            this.MergeAccountStatus(originalAccountStatus, AccountStatus);
            AccountStatusRepository.Update(originalAccountStatus);
            return originalAccountStatus;

        }
        

        [Transaction]
        public void DeleteAccountStatus(int AccountStatusId)
        {
            var AccountStatus = AccountStatusRepository.GetById(AccountStatusId);
            AccountStatusRepository.Delete(AccountStatus);
        }

        [Transaction]
        public IList<AccountStatus> GetAll()
        {
            var AccountStatus = AccountStatusRepository.GetAll();
            if (AccountStatus == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<AccountStatus>();
            var enumerator = AccountStatus.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }



        [Transaction]
        public bool RegisterMonth(int UnitId, int month)
        {
            DateTime now = DateTime.Now;
            var startDate = DateTime.Now.AddYears(-1);// new DateTime(now.Year, month, 1);
            var endDate = now; // startDate.AddMonths(1).AddDays(-1);
           
            var unit = this.UnitRepository.GetById(UnitId);
            var consortiums = this.ConsortiumRepository.GetAll();
            var consortium = consortiums.Where(x => x.Ownership.Id == unit.Ownership.Id).FirstOrDefault();
            if (consortium != null)
            {
                //traer la configuracion de la unidad
                var consortiumConfig = this.ConsortiumConfigurationRepository.GetByConsortiumId(consortium.Id,startDate,endDate).ToList();
                var unitConfig = this.UnitConfigurationRepository.GetByUnitId(UnitId, startDate, endDate).ToList();                

                var auxConsortiumConfig = new List<ConsortiumConfigurationType>();// = consortiumConfig.ForEach
                var auxUnitConfig = new List<UnitConfigurationType>();// = consortiumConfig.ForEach

                foreach (var cc in consortiumConfig)
                {
                    auxConsortiumConfig.Add(cc.Type);
                }

                foreach (var cc in unitConfig)
                {
                    auxUnitConfig.Add(cc.Type);
                }


                var consortiumConfigList = this.MakeConsortiumConfigList(consortiumConfig);
                var unitConfigList = this.MakeUnitConfigList(unitConfig);
                
                decimal currentDebt = 0;
                decimal discount = 0;

                foreach(var uc in unitConfigList)
                {
                    var consConfigAux = consortiumConfigList
                        .Where(x => x.Type.Id == uc.Type.ConsortiumConfigurationType.Id)
                        .OrderByDescending(x => x.ConfigurationDate)
                        .FirstOrDefault();

                    if(consConfigAux != null)
                    {
                        currentDebt += this.CalculateDebtFromConfigurations(uc, consConfigAux);
                    }
                    
                }

                var unitAccount = this.AccountStatusRepository.GetByUnitId(UnitId).ToList();
                var unitPayments = unitAccount.Where(x => x.StatusDate.Year == startDate.Year && x.StatusDate.Month == month && x.IsPayment()).Sum(x => x.Haber);
                var unitDebt = unitAccount.Where(x => x.StatusDate.Year == startDate.Year && x.StatusDate.Month == month && !x.IsPayment() && !x.Interest).Sum(x => x.Debe);
                var unitInterest = unitAccount.Where(x => x.StatusDate.Year == startDate.Year && x.StatusDate.Month == month && !x.IsPayment() && x.Interest).Sum(x => x.Debe);

                var discountConfig = consortiumConfig.Where(x => x.Type.Description == "Descuento por pago adelantado").OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();
                var discountDateConfig = consortiumConfig.Where(x => x.Type.Description == "Día límite pago adelantado").OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();

                if (discountConfig != null && unitAccount.Any(x=>x.IsPayment() && x.StatusDate.Year == startDate.Year && x.StatusDate.Month == month && x.StatusDate.Day < discountDateConfig.Value))
                {
                    discount = currentDebt * (discountConfig.Value / 100);
                }

                var totalPayments = unitPayments - unitInterest;
                var remainignInterest = totalPayments < 0 ? totalPayments * -1 : 0;

                var totalDebt = currentDebt - discount + unitDebt - totalPayments;

                var debtStatus = new AccountStatus()
                {
                    Haber = 0,
                    Debe = totalDebt,
                    Unit = this.UnitRepository.GetById(UnitId),
                    StatusDate = endDate,
                    Interest = false
                };

                this.AccountStatusRepository.Insert(debtStatus);

                remainignInterest += totalDebt > 0 ? totalDebt * INTEREST : 0;
                if (remainignInterest > 0)
                {
                    var interestStatus = new AccountStatus()
                    {
                        Haber = 0,
                        Debe = remainignInterest,
                        Unit = this.UnitRepository.GetById(UnitId),
                        StatusDate = endDate,
                        Interest = true
                    };

                    this.AccountStatusRepository.Insert(interestStatus);
                }
            }
            
            return true;
        }


        private List<ConsortiumConfiguration> MakeConsortiumConfigList(List<ConsortiumConfiguration> configurations)
        {
            var result = new List<ConsortiumConfiguration>();
            var configurationTypes = this.ConsortiumConfigurationTypeRepository.GetAll();
            
            var configDictionary = new Dictionary<int, ConsortiumConfiguration>();

            foreach (var conft in configurationTypes)
            {
                var lastConfig = configurations.Where(x => x.Type.Id == conft.Id)
                    .OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();
                if (lastConfig != null)
                {
                    result.Add(lastConfig);
                }
            }
            return result;

        }

        private List<UnitConfiguration> MakeUnitConfigList(List<UnitConfiguration> configurations)
        {
            var result = new List<UnitConfiguration>();
            var configurationTypes = this.UnitConfigurationTypeRepository.GetAll();
            foreach (var conft in configurationTypes)
            {
                var lastConfig = configurations.Where(x => x.Type.Id == conft.Id)
                    .OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();
                if (lastConfig != null)
                {
                    result.Add(lastConfig);
                }
            }

            return result;

        }


        private decimal CalculateDebtFromConfigurations(UnitConfiguration unitConfig, ConsortiumConfiguration consortiumConfig)
        {
            var result = consortiumConfig.Value * unitConfig.Value / 100;
            return result;
        }


        public AccountStatus ValidateUnique(AccountStatus Account, AccountStatus oldAccountStatus, DateTime startDate, DateTime endDate)
        {
            if (Account != null && (Account.StatusDate >= startDate && Account.StatusDate <= endDate))
            {
                this.AccountStatusRepository.Delete(Account);
                if (Account != oldAccountStatus)
                    return oldAccountStatus;
                else
                    return null;
            }
            return Account;
        }

        public void ValidateSpendsWithTypes(IList<Spend> spends, List<SpendType> spendTypes)
        {
            spendTypes.ForEach(x =>
            {
                if (!spends.Where(y => y.Type.Id == x.Id).Any())
                    throw new Exception("Hay gastos requeridos que no están registrados");
            });

        }

        private void MergeAccountStatus(AccountStatus originalAccountStatus, AccountStatusRequest AccountStatus)
        {            
            originalAccountStatus.Unit = this.UnitRepository.GetById(AccountStatus.UnitId);            
            originalAccountStatus.Debe = AccountStatus.Debe;
            originalAccountStatus.Haber = AccountStatus.Haber;            
            originalAccountStatus.StatusDate = AccountStatus.StatusDate;            
        }

        public decimal GetBalanceByUnitId(int unitId)
        {
            var statusList = this.GetByUnitId(unitId).OrderBy(x => x.StatusDate);
            decimal result = 0;

            foreach(var status in statusList)
            {
                result += status.Haber;
                result -= status.Debe;
            }

            return result;

        }

        public IList<AccountStatus> GetByUnitId(int UnitId)
        {
            return AccountStatusRepository.GetByUnitId(UnitId).ToList();
            
        }

        public IList<UnitAccountStatusSummary> GetConsortiumSummary(int consortiumId, int month)
        {
            var consortium = this.ConsortiumService.GetById(consortiumId);

            var result = new List<UnitAccountStatusSummary>();

            consortium.Ownership.FunctionalUnits.ForEach(x => result.Add(this.GetUnitSummary(x,consortiumId, month)));
          
            return result;            
        }

        public UnitAccountStatusSummary GetUnitSummary(FunctionalUnit unit, int consortiumId, int month)
        {
            DateTime now = DateTime.Now;
            var startDate = DateTime.Now.AddYears(-1);// new DateTime(now.Year, month, 1);
            var endDate = now; // startDate.AddMonths(1).AddDays(-1);


            var consortiumConfig = this.ConsortiumConfigurationService.GetByConsortiumId(consortiumId, startDate, endDate).ToList();

            var unitConfig = this.UnitConfigurationService.GetByUnitId(unit.Id, startDate, endDate).ToList();

            var auxConsortiumConfig = new List<ConsortiumConfigurationType>();// = consortiumConfig.ForEach
            var auxUnitConfig = new List<UnitConfigurationType>();// = consortiumConfig.ForEach

            foreach (var cc in consortiumConfig)
            {
                auxConsortiumConfig.Add(cc.Type);
            }

            foreach (var cc in unitConfig)
            {
                auxUnitConfig.Add(cc.Type);
            }


            var consortiumConfigList = this.MakeConsortiumConfigList(consortiumConfig);
            var unitConfigList = this.MakeUnitConfigList(unitConfig);



            decimal spendA = 0; decimal spendB = 0; decimal edesur = 0; decimal spendC = 0; decimal spendD = 0;
            decimal aysa = 0; decimal discount = 0; decimal expensas = 0;

            foreach (var uc in unitConfigList)
            {
                var consConfigAux = consortiumConfigList
                    .Where(x => x.Type.Id == uc.Type.ConsortiumConfigurationType.Id)
                    .OrderByDescending(x => x.ConfigurationDate)
                    .FirstOrDefault();

                if (consConfigAux != null)
                {
                    var auxdebt = this.CalculateDebtFromConfigurations(uc, consConfigAux);
                    switch (consConfigAux.Type.Description.ToLower())
                    {
                        case "monto a recaudar gastos a":
                            spendA = auxdebt;
                            break;
                        case "monto a recaudar gastos b":
                            spendB = auxdebt;
                            break;
                        case "monto a recaudar gastos c":
                            spendC = auxdebt;
                            break;
                        case "monto a recaudar gastos d":
                            spendD = auxdebt;
                            break;
                        case "monto a recaudar edesur":
                            edesur = auxdebt;
                            break;
                        case "monto a recaudar aysa":
                            aysa = auxdebt;
                            break;
                        case "monto a recaudar expensas extraordinarias":
                            expensas = auxdebt;
                            break;
                    }                    
                }
            }

            var unitAccount = this.AccountStatusRepository.GetByUnitId(unit.Id).ToList();
            var unitPayments = unitAccount.Where(x => x.StatusDate.Year >= startDate.Year && x.StatusDate.Month <= month && x.IsPayment()).Sum(x => x.Haber);
            var unitDebt = unitAccount.Where(x => x.StatusDate.Year >= startDate.Year && x.StatusDate.Month <= (month - 1) && !x.IsPayment() && !x.Interest).Sum(x => x.Debe);
            var unitInterest = unitAccount.Where(x => x.StatusDate.Year == startDate.Year && x.StatusDate.Month == (month - 1) && !x.IsPayment() && x.Interest).Sum(x => x.Debe);
            var currentDebt = spendA + spendB + spendC + spendD + edesur + aysa + expensas;
            var totalUnitDebt = unitDebt + unitInterest;

            var discountConfig = consortiumConfig.Where(x => x.Type.Description == "Descuento por pago adelantado").OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();
            var discountDateConfig = consortiumConfig.Where(x => x.Type.Description == "Día límite pago adelantado").OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();

            if (discountConfig != null && discountDateConfig!=null &&  unitAccount.Any(x => x.IsPayment() && x.StatusDate.Year == startDate.Year && x.StatusDate.Month == month && x.StatusDate.Day < discountDateConfig.Value))
            {
                discount = currentDebt * (discountConfig.Value / 100);
            }

            decimal lastBalance = 0;
            var lastsDebts = unitAccount.Where(x => x.StatusDate.Year >= startDate.Year && x.StatusDate.Month <= (month - 2) && !x.IsPayment() && !x.Interest).Sum(x => x.Debe);
            var lastsPayments = unitAccount.Where(x => x.StatusDate.Year >= startDate.Year && x.StatusDate.Month <= (month -1)  && x.IsPayment()).Sum(x => x.Haber);
            lastBalance = lastsDebts - lastsPayments;
            //revisar

            var result = new UnitAccountStatusSummary()
            {
                Uf = unit.Number.ToString(),
                Propietario = unit.Owner != null ? unit.Owner.User.Name+" "+unit.Owner.User.Surname : string.Empty,
                SaldoAnterior = lastBalance,
                Deuda = currentDebt,
                Pagos = unitPayments,
                Aysa = aysa,
                Edesur = edesur,
                Expensas = expensas,
                GastosA = spendA,
                GastosB = spendB,
                GastosC = spendC,
                GastosD = spendD,
                Dto = unit.Dto,
                Piso = unit.Floor.ToString(),
                SiPagaAntes = discount,
                Intereses = unitInterest,
                Total = totalUnitDebt + currentDebt - discount - unitPayments,
                DiscountDay = discountConfig != null ? discountConfig.ConfigurationDate.Day : (int?)null,
                DiscountValue = discountConfig != null ? discountConfig.Value : (decimal?)null
            };

            return result;
            
        }
    }
}
