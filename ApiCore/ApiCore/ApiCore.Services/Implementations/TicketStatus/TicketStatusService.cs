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
using System.Linq;

namespace ApiCore.Services.Implementations.TicketStatus
{
    public class TicketStatusService : ITicketStatusService
    {
        public IStatusRepository TicketStatusRepository { get; set; }
        
        
        [Transaction]
        public IList<DomainModel.TicketStatus> GetAll()
        {
            return TicketStatusRepository.GetAll().ToList();            
        }
        

    }
}
