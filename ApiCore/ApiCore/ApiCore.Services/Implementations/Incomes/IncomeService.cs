using ApiCore.Services.Contracts.Incomes;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Incomes
{
    public class IncomeService : IIncomeService
    {
        public IIncomeRepository IncomeRepository { get; set; }
        public IIncomeTypeRepository IncomeTypeRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }        
        public IBillRepository BillRepository { get; set; }
        
        [Transaction]
        public Income CreateIncome(IncomeRequest Income)
        {
            var entityToInsert = new Income() { };
            MergeIncome(entityToInsert, Income);
            IncomeRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Income GetById(int IncomeId)
        {
            var Income = IncomeRepository.GetById(IncomeId);
            if (Income == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return Income;
        }
        

        [Transaction]
        public Income UpdateIncome(Income originalIncome, IncomeRequest Income)
        {            
            this.MergeIncome(originalIncome, Income);
            IncomeRepository.Update(originalIncome);
            return originalIncome;

        }
        

        [Transaction]
        public void DeleteIncome(int IncomeId)
        {
            var Income = IncomeRepository.GetById(IncomeId);
            IncomeRepository.Delete(Income);
        }

        [Transaction]
        public IList<Income> GetAll()
        {
            var Incomes = IncomeRepository.GetAll();
            if (Incomes == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<Income>();
            var enumerator = Incomes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

        private void MergeIncome(Income originalIncome, IncomeRequest Income)
        {            
            originalIncome.Consortium = this.ConsortiumRepository.GetById(Income.ConsortiumId);
            originalIncome.Type = this.IncomeTypeRepository.GetById(Income.IncomeTypeId);
            originalIncome.Amount = Income.Amount;
            originalIncome.Description = Income.Description;
            originalIncome.IncomeDate = Income.IncomeDate;            
        }

        public IList<Income> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            return IncomeRepository.GetByConsortiumId(consortiumId, startDate, endDate).ToList();
            
        }
    }
}
