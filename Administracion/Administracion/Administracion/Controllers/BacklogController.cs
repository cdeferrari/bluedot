using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Implementations.Tickets;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    //[Authorize]
    public class BacklogController : Controller
    {
        public virtual ITicketService TicketService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Backlog
        public ActionResult CreateTicket()
        {
            return View();
        }

        public ActionResult CreateNewTicket(TicketViewModel ticket)
        {
         
            var nticket = new Ticket(); 
            //this.MapTicket(nticket, ticket);
            nticket = Mapper.Map<Ticket>(ticket);
            try
            {
                this.TicketService.CreateTicket(nticket);
                return View("CreateSuccess");
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


        public ActionResult UpdateTicketById(int id)
        {
            var oTicket = this.TicketService.GetTicket(id);
            var ticket = Mapper.Map<TicketViewModel>(oTicket);            
            return View("CreateTicket",ticket);
        }

        public ActionResult UpdateTicket(TicketViewModel ticket)
        {            
            var nticket = new Ticket();
            ///this.MapTicket(nticket, ticket);
            nticket = Mapper.Map<Ticket>(ticket);            
            this.TicketService.UpdateTicket(nticket);
            return View();
        }

        public ActionResult DeleteTicket(int id)
        {                    
            this.TicketService.DeleteTicket(id);
            return View();
        }
        
        public ActionResult List()
        {         
         
            try
            {
                var tickets = this.TicketService.GetAll();
                var ticketsViewModel = Mapper.Map<List<TicketViewModel>>(tickets);
                return View(ticketsViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


    }
}