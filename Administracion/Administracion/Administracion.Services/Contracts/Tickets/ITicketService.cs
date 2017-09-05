using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Tickets
{
    public interface ITicketService
    {
        Ticket GetTicket(int ticketId);
        bool CreateTicket(Ticket ticket);
        bool UpdateTicket(Ticket ticket);
        bool DeleteTicket(int ticketId);
    }
}
