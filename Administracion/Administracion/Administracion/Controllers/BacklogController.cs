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
            TicketService = new TicketService();
            this.TicketService.CreateTicket(new Ticket()
            {
                

            });
            return View();
        }

    }
}