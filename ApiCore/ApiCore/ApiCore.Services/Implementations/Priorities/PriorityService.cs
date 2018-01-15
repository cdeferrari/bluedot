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
using ApiCore.Services.Contracts.Priorities;
using System.Linq;

namespace ApiCore.Services.Implementations.Priorities
{
    public class PriorityService : IPriorityService
    {
        public IPriorityRepository PriorityRepository { get; set; }
        
        
        [Transaction]
        public IList<Priority> GetAll()
        {
            return PriorityRepository.GetAll().ToList();
            
        }
        
        
    }
}
