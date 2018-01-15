using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.LaboralUnion;
using System.Linq;

namespace ApiCore.Services.Implementations.LaboralUnion
{
    public class LaboralUnionService : ILaboralUnionService
    {
        public ILaboralUnionRepository LaboralUnionRepository { get; set; }
        [Transaction]
        public DomainModel.LaboralUnion CreateLaboralUnion(DescriptionRequest LaboralUnion)
        {
            var entity = new DomainModel.LaboralUnion() { Description = LaboralUnion.Description };
            this.LaboralUnionRepository.Insert(entity);
            return entity;
        }
        [Transaction]
        public void Delete(int laboralUnionId)
        {
            var entity = this.LaboralUnionRepository.GetById(laboralUnionId);
            this.LaboralUnionRepository.Delete(entity);
        }

        [Transaction]
        public IList<DomainModel.LaboralUnion> GetAll()
        {
            return LaboralUnionRepository.GetAll().ToList();
        }
        

    }
}
