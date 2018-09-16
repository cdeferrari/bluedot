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

namespace ApiCore.Services.Implementations.AccountStatuss
{
    public class AccountStatusService : IAccountStatusService
    {
        public IAccountStatusRepository AccountStatusRepository { get; set; }        
        public IFunctionalUnitRepository UnitRepository { get; set; }
        public ISpendRepository SpendRepository { get; set; }
        public IIncomeRepository IncomeRepository { get; set; }
        public ISpendTypeRepository SpendTypeRepository { get; set; }

        public IConsortiumService ConsortiumService { get; set; }
        public IConsortiumConfigurationsService ConsortiumConfigurationService { get; set; }
        public IUnitConfigurationsService UnitConfigurationService { get; set; }

        [Transaction]
        public AccountStatus CreateAccountStatus(AccountStatusRequest AccountStatus)
        {
            var entityToInsert = new AccountStatus() { };
            MergeAccountStatus(entityToInsert, AccountStatus);
            AccountStatusRepository.Insert(entityToInsert);
            return entityToInsert;
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
            var consortiums = this.ConsortiumService.GetAll();
            var consortium = consortiums.Where(x => x.Ownership.Id == unit.Ownership.Id).FirstOrDefault();
            if (consortium != null)
            {
                //traer la configuracion de la unidad
                var consortiumConfig = this.ConsortiumConfigurationService.GetByConsortiumId(consortium.Id,startDate,endDate);
                var unitConfig = this.UnitConfigurationService.GetByUnitId(UnitId, startDate, endDate);
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

        public IList<AccountStatus> GetByUnitId(int UnitId)
        {
            return AccountStatusRepository.GetByUnitId(UnitId).ToList();
            
        }
    }
}
