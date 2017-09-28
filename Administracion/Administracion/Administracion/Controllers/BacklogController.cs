using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Priorities;
using Administracion.Services.Contracts.Status;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Contracts.Workers;
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
        public virtual IStatusService StatusService { get; set; }
        public virtual IPriorityService PriorityService { get; set; }
        public virtual IUserService UserService { get; set; }
        public virtual IWorkerService WorkerService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IFunctionalUnitService FunctionalUnitService { get; set; }



        public ActionResult Index()
        {
            return View();
        }

        // GET: Backlog
        public ActionResult CreateTicket()
        {
            var statusList = this.StatusService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var priorityList = this.PriorityService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });


            var userList = this.UserService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name + " " + x.Surname
            });

            var workersList = this.WorkerService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var consortiumList = this.ConsortiumService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FriendlyName
            });

            var functionalUnitList = this.FunctionalUnitService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Address.Street+" "+ x.Address.Number
            });




            var viewModel = new TicketViewModel()
            {
                PriorityList = priorityList,
                StatusList = statusList,
                WorkersList= workersList,
                UsersList = userList
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateTicket(TicketViewModel ticket)
        {                     
            var nticket = Mapper.Map<Ticket>(ticket);
            try
            {
                if (nticket.Id != 0)
                {
                    this.TicketService.CreateTicket(nticket);
                }
                else
                {
                    this.TicketService.UpdateTicket(nticket);
                }
                return View("CreateSuccess");
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


        public ActionResult UpdateTicketById(int id)
        {
            var statusList = this.StatusService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var priorityList = this.PriorityService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var userList = this.UserService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name+" "+x.Surname
            });

            var workersList = this.WorkerService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });



            var oTicket = this.TicketService.GetTicket(id);
            var ticket = Mapper.Map<TicketViewModel>(oTicket);
            ticket.StatusList = statusList;
            ticket.PriorityList = priorityList;
            ticket.WorkersList = workersList;
            ticket.UsersList = userList;
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