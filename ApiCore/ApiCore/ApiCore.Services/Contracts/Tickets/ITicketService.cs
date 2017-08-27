using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Tickets
{
    interface ITicketService
    {
        Ticket CreateTicket(TicketRequest ticket);
        Ticket GetById(int ticketId);
        Ticket UpdateTicket(Ticket originalTicket, TicketRequest ticket);
        void DeleteTicket(int ticketId);
    }
}
