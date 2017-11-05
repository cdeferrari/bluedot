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

namespace ApiCore.Services.Implementations.LaboralUnion
{
    public class LaboralUnionService : ILaboralUnionService
    {
        public IStatusRepository LaboralUnionRepository { get; set; }
        
        
        [Transaction]
        public IList<DomainModel.LaboralUnion> GetAll()
        {
            var status = LaboralUnionRepository.GetAll();
            if (status == null)
                throw new BadRequestException(ErrorMessages.TicketNoEncontrado);

            var result = new List<DomainModel.LaboralUnion>();
            var enumerator = status.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

    }
}
