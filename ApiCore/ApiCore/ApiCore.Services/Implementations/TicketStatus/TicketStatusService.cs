using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.TicketStatus;

namespace ApiCore.Services.Implementations.TicketStatus
{
    public class TicketStatusService : ITicketStatusService
    {
        public IStatusRepository TicketStatusRepository { get; set; }
        
        
        [Transaction]
        public IList<DomainModel.TicketStatus> GetAll()
        {
            var status = TicketStatusRepository.GetAll();
            if (status == null)
                throw new BadRequestException(ErrorMessages.TicketNoEncontrado);

            var result = new List<DomainModel.TicketStatus>();
            var enumerator = status.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

    }
}
