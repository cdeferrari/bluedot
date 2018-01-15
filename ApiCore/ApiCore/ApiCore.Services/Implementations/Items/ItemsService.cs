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
    public class ItemsService : IItemsService
    {
        public IItemsRepository ItemsRepository { get; set; }
        
        
        [Transaction]
        public IList<Item> GetAll()
        {
            return ItemsRepository.GetAll().ToList();
        }
        

    }
}
