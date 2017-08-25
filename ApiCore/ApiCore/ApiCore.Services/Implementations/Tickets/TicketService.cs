using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;

namespace ApiCore.Services.Implementations.Tickets
{
    public class TicketService : ITicketService
    {
        public ITicketRepository TicketRepository { get; set; }

        public Ticket CreateTicket(TicketRequest ticket)
        {
            var entityToInsert = new Ticket() { };
            TicketRepository.Insert(entityToInsert);
            return entityToInsert;
        }
    }
}
