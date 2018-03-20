using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.IncomeTypes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;

namespace ApiCore.Services.Implementations.IncomeTypes
{

    public class IncomeTypeService : IIncomeTypeService
    {
        public IIncomeTypeRepository IncomeTypeRepository { get; set; } 
        public IConsortiumRepository ConsortiumRepository { get; set; }


        public IncomeType GetById(int IncomeTypeId)
        {
            var Income = IncomeTypeRepository.GetById(IncomeTypeId);
            if (Income == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return Income;
        }


        [Transaction]
        public IncomeType CreateIncomeType(IncomeTypeRequest IncomeType)
        {
            var entity = new IncomeType();
            this.MergeIncomeType(entity, IncomeType);            
            this.IncomeTypeRepository.Insert(entity);
            return entity;
        }
        [Transaction]
        public void Delete(int IncomeTypeId)
        {
            var entity = this.IncomeTypeRepository.GetById(IncomeTypeId);
            this.IncomeTypeRepository.Delete(entity);
        }

        [Transaction]
        public IList<IncomeType> GetAll()
        {
            return IncomeTypeRepository.GetAll().ToList();
            
        }

        [Transaction]
        public IncomeType UpdateIncomeType(IncomeType originalIncome, IncomeTypeRequest Income)
        {
            this.MergeIncomeType(originalIncome, Income);
            IncomeTypeRepository.Update(originalIncome);
            return originalIncome;

        }

        private void MergeIncomeType(IncomeType originalIncomeType, IncomeTypeRequest IncomeType)
        {            
            originalIncomeType.Consortium = this.ConsortiumRepository.GetById(IncomeType.ConsortiumId);
            originalIncomeType.Description = IncomeType.Description;
            originalIncomeType.Required = IncomeType.Required;            
        }

    }
}
