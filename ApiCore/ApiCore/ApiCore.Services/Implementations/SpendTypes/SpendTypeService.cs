using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using System.Collections.Generic;
using ApiCore.Dtos;
using System.Linq;
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;

namespace ApiCore.Services.Implementations.SpendTypes
{

    public class SpendTypeService : ISpendTypeService
    {
        public ISpendTypeRepository SpendTypeRepository { get; set; }
        public ISpendItemRepository SpendItemRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }


        public SpendType GetById(int SpendTypeId)
        {
            var Spend = SpendTypeRepository.GetById(SpendTypeId);
            if (Spend == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return Spend;
        }


        [Transaction]
        public SpendType CreateSpendType(SpendTypeRequest SpendType)
        {   
            var entity = new SpendType();
            this.MergeSpendType(entity, SpendType);            
            this.SpendTypeRepository.Insert(entity);


            if (SpendType.ForAll)
            {
                var consortiums = this.ConsortiumRepository.GetAll().Where(x => x.Id != SpendType.ConsortiumId)
                    .Select(x => x.Id)
                    .ToList();

                consortiums.ForEach(x => 
                {
                    var nentity = new SpendType();
                    this.MergeSpendType(nentity, SpendType);
                    nentity.Consortium = this.ConsortiumRepository.GetById(x);
                    this.SpendTypeRepository.Insert(nentity);
                });                
            }
            
            return entity;
        }
        [Transaction]
        public void Delete(int SpendTypeId)
        {
            var entity = this.SpendTypeRepository.GetById(SpendTypeId);
            this.SpendTypeRepository.Delete(entity);
        }

        [Transaction]
        public IList<SpendType> GetAll()
        {
            return SpendTypeRepository.GetAll().ToList();
            
        }

        [Transaction]
        public SpendType UpdateSpendType(SpendType originalSpend, SpendTypeRequest Spend)
        {
            this.MergeSpendType(originalSpend, Spend);
            SpendTypeRepository.Update(originalSpend);
            return originalSpend;

        }

        private void MergeSpendType(SpendType originalSpendType, SpendTypeRequest SpendType)
        {            
            originalSpendType.Consortium = this.ConsortiumRepository.GetById(SpendType.ConsortiumId);
            originalSpendType.Description = SpendType.Description;
            originalSpendType.Required = SpendType.Required;
            originalSpendType.Item = this.SpendItemRepository.GetById(SpendType.SpendItemId);
        }

    }
}
