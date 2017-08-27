using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;

namespace ApiCore.Services.Implementations.Tickets
{
    public class TicketService : ITicketService
    {
        public ITicketRepository TicketRepository { get; set; }

        [Transaction]
        public Ticket CreateTicket(TicketRequest ticket)
        {
            var entityToInsert = new Ticket() { };
            TicketRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Ticket GetById(int ticketId)
        {
            var ticket = TicketRepository.GetById(ticketId);
            if (ticket == null)
                throw new BadRequestException(ErrorMessages.TicketNoEncontrado);

            return ticket;
        }
        

        [Transaction]
        public Ticket UpdateTicket(Ticket originalTicket, TicketRequest ticket)
        {            
            this.MergeTicket(originalTicket, ticket);
            TicketRepository.Update(originalTicket);
            return originalTicket;

        }
        

        [Transaction]
        public void DeleteTicket(int ticketId)
        {
            var ticket = TicketRepository.GetById(ticketId);
            TicketRepository.Delete(ticket);
        }

        #region private Methods 

        private void MergeTicket(Ticket originalTicket, TicketRequest ticket)
        {
            originalTicket.Customer = ticket.Customer;
            originalTicket.ConsortiumId = ticket.ConsortiumId;
            originalTicket.AdministrationId = ticket.AdministrationId;
            originalTicket.StatusId = ticket.StatusId;
            originalTicket.OpenDate = ticket.OpenDate;
            originalTicket.CloseDate = ticket.CloseDate;
            originalTicket.LimitDate = ticket.LimitDate;
            originalTicket.FunctionalUnitId = ticket.FunctionalUnitId;
            originalTicket.Priority = ticket.Priority;
            originalTicket.WorkerId = ticket.WorkerId;
            originalTicket.CreatorId = ticket.CreatorId;
        }

        #endRegion

    }
}
