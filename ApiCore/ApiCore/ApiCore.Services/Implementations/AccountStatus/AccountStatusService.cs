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
using ApiCore.Services.Contracts.Spends;
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.Services.Contracts.Incomes;
using ApiCore.Services.Contracts.ConsortiumConfigurations;
using ApiCore.Services.Contracts.UnitConfigurations;
using ApiCore.Services.Contracts.Consortiums;
using ApiCore.Services.Contracts.Unit;
using ApiCore.Dtos.Response;

namespace ApiCore.Services.Implementations.AccountStatuss
{
    public class AccountStatusService : IAccountStatusService
    {
        public IPaymentRepository PaymentRepository { get; set; }
        public IPaymentTypeRepository PaymentTypeRepository { get; set; }
        public IAccountStatusRepository AccountStatusRepository { get; set; }        
        public IFunctionalUnitRepository UnitRepository { get; set; }
        public ISpendRepository SpendRepository { get; set; }
        public IIncomeRepository IncomeRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }
        public ISpendTypeRepository SpendTypeRepository { get; set; }
        public IConsortiumConfigurationRepository ConsortiumConfigurationRepository { get; set; }
        public IUnitConfigurationRepository UnitConfigurationRepository { get; set; }        
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
        public bool RegisterMonth(int UnitId)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
           
            var unit = this.UnitRepository.GetById(UnitId);
            var consortiums = this.ConsortiumRepository.GetAll();
            var consortium = consortiums.Where(x => x.Ownership.Id == unit.Ownership.Id).FirstOrDefault();
            if (consortium != null)
            {
                //traer la configuracion de la unidad
                var consortiumConfig = this.ConsortiumConfigurationRepository.GetByConsortiumId(consortium.Id,startDate,endDate);
                var unitConfig = this.UnitConfigurationRepository.GetByUnitId(UnitId, startDate, endDate);
                decimal debt = 0;

                foreach(var uc in unitConfig)
                {
                    var consConfigAux = consortiumConfig
                        .Where(x => x.Type.Id == uc.Type.ConsortiumConfigurationType.Id)
                        .OrderByDescending(x => x.ConfigurationDate)
                        .FirstOrDefault();

                    if(consConfigAux != null)
                    {
                        var auxdebt = this.CalculateDebtFromConfigurations(uc, consConfigAux);
                        debt += auxdebt;
                    }
                    
                }
                
                var AccountStatus = new AccountStatus()
                {
                    Haber = 0,
                    Debe = debt,
                    Unit = this.UnitRepository.GetById(UnitId),
                    StatusDate = now
                };

                this.AccountStatusRepository.Insert(AccountStatus);
            }
            
            return true;
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

        public IList<UnitAccountStatusSummary> GetConsortiumSummary(int consortiumId)
        {
            var consortium = this.ConsortiumService.GetById(consortiumId);

            var result = new List<UnitAccountStatusSummary>();

            consortium.Ownership.FunctionalUnits.ForEach(x => result.Add(this.GetUnitSummary(x,consortiumId)));
          
            return result;            
        }

        public UnitAccountStatusSummary GetUnitSummary(FunctionalUnit unit, int consortiumId)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var consortiumConfig = this.ConsortiumConfigurationService.GetByConsortiumId(consortiumId, startDate, endDate);

            var unitConfig = this.UnitConfigurationService.GetByUnitId(unit.Id, startDate, endDate);
            decimal debt = 0; decimal spendA = 0; decimal spendB = 0; decimal edesur = 0;
            decimal aysa = 0; decimal descAmount = 0; decimal expensas = 0;

            var descAmountConfig = consortiumConfig.Where(x => x.Type.Description == "Descuento por pago adelantado").OrderByDescending(x => x.ConfigurationDate)
                    .FirstOrDefault();
            
            if(descAmountConfig != null)
            {
                descAmount = descAmountConfig.Value;
            }


            foreach (var uc in unitConfig)
            {
                var consConfigAux = consortiumConfig
                    .Where(x => x.Type.Id == uc.Type.ConsortiumConfigurationType.Id)
                    .OrderByDescending(x => x.ConfigurationDate)
                    .FirstOrDefault();

                if (consConfigAux != null)
                {
                    var auxdebt = this.CalculateDebtFromConfigurations(uc, consConfigAux);
                    switch (consConfigAux.Type.Description)
                    {
                        case "Monto a reacaudar gasto tipo A":
                            spendA = auxdebt;
                            break;
                        case "Monto a reacaudar gasto tipo B":
                            spendB = auxdebt;
                            break;
                        case "Monto a recaudar edesur":
                            edesur = auxdebt;
                            break;
                        case "Monto a recaudar aysa":
                            aysa = auxdebt;
                            break;
                        case "Monto a recaudar expensas extraordinarias":
                            expensas = auxdebt;
                            break;
                    }
                    
                }

            }


            var allStatus = this.GetByUnitId(unit.Id);
            var lastStatus = allStatus
                .Where(x => x.Debe > 0)
                .OrderByDescending(x => x.StatusDate)
                .FirstOrDefault();

            var debe = allStatus.Sum(x => x.Debe);
            var haber = allStatus.Sum(x => x.Haber);

            //revisar
            var result = new UnitAccountStatusSummary()
            {
                Uf = unit.Number.ToString(),
                Propietario = unit.Owner != null ? unit.Owner.User.Name+" "+unit.Owner.User.Surname : string.Empty,
                SaldoAnterior =lastStatus != null ?  lastStatus.Debe - debe : debe,
                Deuda = debe - haber,
                Pagos = haber,
                Aysa = aysa,
                Edesur = edesur,
                Expensas = expensas,
                GastosA = spendA,
                GastosB = spendB,
                Dto = unit.Dto,
                Piso = unit.Floor.ToString(),
                SiPagaAntes = (debe - haber) - ((debe-haber) * descAmount / 100),
                Intereses = 0,
                Total = debe - haber                
            };

            return result;
            
        }
    }
}
