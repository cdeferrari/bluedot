using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Tickets
{
    public interface ITicketService
    {
        Ticket CreateTicket(TicketRequest ticket);
        Ticket GetById(int ticketId);
        Ticket UpdateTicket(Ticket originalTicket, TicketRequest ticket);
        void DeleteTicket(int ticketId);
    }
}
