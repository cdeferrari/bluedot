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
    public class ItemsService : IItemsService
    {
        public IItemsRepository ItemsRepository { get; set; }
        
        
        [Transaction]
        public IList<Item> GetAll()
        {
            var status = ItemsRepository.GetAll();
            if (status == null)
                throw new BadRequestException(ErrorMessages.TicketNoEncontrado);

            var result = new List<Item>();
            var enumerator = status.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

    }
}
