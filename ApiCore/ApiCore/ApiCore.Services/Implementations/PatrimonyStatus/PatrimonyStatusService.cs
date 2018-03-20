using ApiCore.Services.Contracts.PatrimonyStatuss;
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

namespace ApiCore.Services.Implementations.PatrimonyStatuss
{
    public class PatrimonyStatusService : IPatrimonyStatusService
    {
        public IPatrimonyStatusRepository PatrimonyStatusRepository { get; set; }        
        public IConsortiumRepository ConsortiumRepository { get; set; }
        public ISpendRepository SpendRepository { get; set; }
        public IIncomeRepository IncomeRepository { get; set; }
        public ISpendTypeRepository SpendTypeRepository { get; set; }

        [Transaction]
        public PatrimonyStatus CreatePatrimonyStatus(PatrimonyStatusRequest PatrimonyStatus)
        {
            var entityToInsert = new PatrimonyStatus() { };
            MergePatrimonyStatus(entityToInsert, PatrimonyStatus);
            PatrimonyStatusRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public PatrimonyStatus GetById(int PatrimonyStatusId)
        {
            var PatrimonyStatus = PatrimonyStatusRepository.GetById(PatrimonyStatusId);
            if (PatrimonyStatus == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return PatrimonyStatus;
        }
        

        [Transaction]
        public PatrimonyStatus UpdatePatrimonyStatus(PatrimonyStatus originalPatrimonyStatus, PatrimonyStatusRequest PatrimonyStatus)
        {            
            this.MergePatrimonyStatus(originalPatrimonyStatus, PatrimonyStatus);
            PatrimonyStatusRepository.Update(originalPatrimonyStatus);
            return originalPatrimonyStatus;

        }
        

        [Transaction]
        public void DeletePatrimonyStatus(int PatrimonyStatusId)
        {
            var PatrimonyStatus = PatrimonyStatusRepository.GetById(PatrimonyStatusId);
            PatrimonyStatusRepository.Delete(PatrimonyStatus);
        }

        [Transaction]
        public IList<PatrimonyStatus> GetAll()
        {
            var PatrimonyStatus = PatrimonyStatusRepository.GetAll();
            if (PatrimonyStatus == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<PatrimonyStatus>();
            var enumerator = PatrimonyStatus.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }


        [Transaction]
        public bool RegisterMonth(int consortiumId)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            
            var spends = this.SpendRepository.GetByConsortiumId(consortiumId, startDate, endDate);

            var incomes = this.IncomeRepository.GetByConsortiumId(consortiumId, startDate, endDate);

            var spendTypes = this.SpendTypeRepository.GetByConsortiumId(consortiumId).Where(x => x.Required).ToList();

            this.ValidateSpendsWithTypes(spends, spendTypes);
            
            var lastStatus = this.PatrimonyStatusRepository
                .GetByConsortiumId(spends.FirstOrDefault().Consortium.Id)
                .OrderByDescending(x => x.StatusDate)
                .FirstOrDefault();

            this.ValidateUnique(lastStatus, startDate, endDate);

            var debt = spends.Sum(x => x.Bill.Amount);
            var totalIncome = incomes.Sum(x => x.Amount);

            var patrimonyStatus = new PatrimonyStatus()
            {
                Activo = lastStatus != null ? lastStatus.Activo - debt + totalIncome : 0 - debt + totalIncome,
                Pasivo = lastStatus != null ? lastStatus.Pasivo + debt - totalIncome : 0 + debt - totalIncome,
                Haber = totalIncome,
                Debe = debt,
                Consortium = lastStatus != null ? lastStatus.Consortium : this.ConsortiumRepository.GetById(consortiumId),
                StatusDate = now           
            };

            this.PatrimonyStatusRepository.Insert(patrimonyStatus);

            return true;
        }


        public void ValidateUnique(PatrimonyStatus patrimony, DateTime startDate, DateTime endDate)
        {
            if(patrimony != null && (patrimony.StatusDate > startDate && patrimony.StatusDate < endDate))
                throw new Exception("El mes ya se encuentra cerrado");

        }

        public void ValidateSpendsWithTypes(IList<Spend> spends, List<SpendType> spendTypes)
        {
            spendTypes.ForEach(x =>
            {
                if (!spends.Where(y => y.Type.Id == x.Id).Any())
                    throw new Exception("Hay gastos requeridos que no están registrados");
            });

        }

        private void MergePatrimonyStatus(PatrimonyStatus originalPatrimonyStatus, PatrimonyStatusRequest PatrimonyStatus)
        {            
            originalPatrimonyStatus.Consortium = this.ConsortiumRepository.GetById(PatrimonyStatus.ConsortiumId);            
            originalPatrimonyStatus.Activo = PatrimonyStatus.Activo;
            originalPatrimonyStatus.Pasivo = PatrimonyStatus.Pasivo;
            originalPatrimonyStatus.Debe = PatrimonyStatus.Debe;
            originalPatrimonyStatus.Haber = PatrimonyStatus.Haber;            
            originalPatrimonyStatus.StatusDate = PatrimonyStatus.StatusDate;            
        }

        public IList<PatrimonyStatus> GetByConsortiumId(int consortiumId)
        {
            return PatrimonyStatusRepository.GetByConsortiumId(consortiumId).ToList();
            
        }
    }
}
