using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Implementations.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    public class BacklogController : Controller
    {
        public ITicketService TicketService { get; set; }
        // GET: Backlog
        public ActionResult CreateTicket()
        {
            return View();
        }

        public ActionResult CreateTicket(TicketViewModel ticket)
        {
         
            var nticket = new Ticket();
            //this.MapTicket(nticket, ticket);
            var nticket = Mapper.Map<Ticket>(ticket);
            this.TicketService.CreateTicket(nticket);
            
            return View();
        }


        public ActionResult UpdateTicket(int id)
        {
            var oTicket = this.TicketService.GetTicket(id);
            var ticket = Mapper.Map<TicketViewModel>(oTicket);            
            return View(ticket);
        }

        public ActionResult UpdateTicket(TicketViewModel ticket)
        {            
            var nticket = new Ticket();
            ///this.MapTicket(nticket, ticket);
            var nticket = Mapper.Map<Ticket>(ticket);            
            this.TicketService.UpdateTicket(nticket);
            return View();
        }

        public ActionResult DeleteTicket(int id)
        {                    
            this.TicketService.DeleteTicket(id);
            return View();
        }

        private void MapTicket(TicketViewModel ticket, Ticket nticket)
        {
                nticket.Id = ticket.Id;                
                nticket.Customer = ticket.Customer;
                nticket.ConsortiumId = ticket.ConsortiumId;
                nticket.StatusId = ticket.StatusId;
                nticket.OpenDate = ticket.OpenDate;
                nticket.CloseDate = ticket.CloseDate;
                nticket.LimitDate = ticket.LimitDate;
                nticket.FunctionalUnitId = ticket.FunctionalUnitId;
                nticket.PriorityId = ticket.PriorityId;
                nticket.WorkerId= ticket.WorkerId;
                nticket.CreatorId = ticket.CreatorId;            

        }


    }
}