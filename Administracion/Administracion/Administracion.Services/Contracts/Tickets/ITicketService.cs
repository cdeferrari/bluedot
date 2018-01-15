using Administracion.DomainModel;
using Administracion.Dto.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Tickets
{
    public interface ITicketService
    {
        IList<Ticket> GetAll();
        IList<Ticket> GetByConsortiumId(int consortiumId);
        Ticket GetTicket(int ticketId);
        bool CreateTicket(TicketRequest ticket);
        bool UpdateTicket(TicketRequest ticket);
        bool DeleteTicket(int ticketId);
    }
}
