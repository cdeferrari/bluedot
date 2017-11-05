using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.List;
using Administracion.Dto.Ticket;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Autentication;
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
    [CustomAuthorize(Roles.All)]
    public class BacklogController : Controller
    {
        public virtual ITicketService TicketService { get; set; }
        public virtual IStatusService StatusService { get; set; }
        public virtual IPriorityService PriorityService { get; set; }
        public virtual IUserService UserService { get; set; }
        public virtual IAuthentication AuthenticationService { get; set; }
        public virtual IWorkerService WorkerService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IFunctionalUnitService FunctionalUnitService { get; set; }



        public ActionResult Index()
        {

            try
            {
                var tickets = this.TicketService.GetAll();
                var ticketsViewModel = Mapper.Map<List<TicketViewModel>>(tickets);
                return View("List",ticketsViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
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


            var userList = this.AuthenticationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
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
                Text = x.Dto.ToString()
            });


            var viewModel = new TicketViewModel()
            {
                PriorityList = priorityList,
                StatusList = statusList,
                WorkersList = workersList,
                UsersList = userList,
                ConsortiumList = consortiumList,
                FunctionalUnitList = functionalUnitList

            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateTicket(TicketViewModel ticket)
        {                     
            var nticket = Mapper.Map<TicketRequest>(ticket);
            try
            {
                var result = false;
                if (nticket.Id == 0)
                {
                    result = this.TicketService.CreateTicket(nticket);
                }
                else
                {
                    result = this.TicketService.UpdateTicket(nticket);
                }
                if (result)
                {
                    return Redirect("/Backlog/Index");
                }
                else
                {
                    return View("../Shared/Error");
                }
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

            var userList = this.AuthenticationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
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
                Text = x.Dto.ToString()
            });

            var oTicket = this.TicketService.GetTicket(id);
            var ticket = Mapper.Map<TicketViewModel>(oTicket);
            ticket.StatusList = statusList;
            ticket.PriorityList = priorityList;
            ticket.WorkersList = workersList;
            ticket.UsersList = userList;
            ticket.ConsortiumList = consortiumList;
            ticket.FunctionalUnitList = functionalUnitList;
            return View("CreateTicket",ticket);
        }

        public ActionResult UpdateTicket(TicketViewModel ticket)
        {                        
            var nticket = Mapper.Map<TicketRequest>(ticket);            
            this.TicketService.UpdateTicket(nticket);
            return View();
        }

        public ActionResult DeleteTicket(int id)
        {                    
            this.TicketService.DeleteTicket(id);

            return Redirect("/Backlog/Index");
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