using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.List;
using Administracion.Dto.Ticket;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Autentication;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Managers;
using Administracion.Services.Contracts.Priorities;
using Administracion.Services.Contracts.Providers;
using Administracion.Services.Contracts.SpendItemsService;
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
        public virtual IProviderService ProviderService { get; set; }
        public virtual IManagerService ManagerService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IFunctionalUnitService FunctionalUnitService { get; set; }
        public virtual ISpendItemsService SpendItemService { get; set; }


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
            var statusList = this.StatusService.GetAll();
            var statusSelectList = statusList
                .Select(x => new SelectListItem()
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

            var providerList = this.ProviderService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var managerList = this.ManagerService.GetAll().Select(x => new SelectListItem()
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
                Text = x.Dto != null ? x.Dto.ToString() : "-"
            });
            
            var viewModel = new TicketViewModel()
            {
                PriorityList = priorityList,
                StatusList = statusSelectList,
                WorkersList = workersList,
                ManagerList = managerList,
                ProviderList = providerList,
                UsersList = userList,
                ConsortiumList = consortiumList,
                FunctionalUnitList = functionalUnitList

            };
            viewModel.Status = new Status() { Id = statusList.Where(x => x.Description.Equals("open")).FirstOrDefault().Id };
            viewModel.LimitDate = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateTicket(TicketViewModel ticket)
        {
            var statusList = this.StatusService.GetAll();
            var nticket = Mapper.Map<TicketRequest>(ticket);
            nticket.CreatorId = SessionPersister.Account.Id;
            nticket.OpenDate = DateTime.Now;
            nticket.StatusId = statusList.Where(x => x.Description.Equals("open")).FirstOrDefault().Id;

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

            var providerList = this.ProviderService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var managerList = this.ManagerService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var spendItemsList = this.SpendItemService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var allConsortiums = this.ConsortiumService.GetAll();

            var consortiumList = allConsortiums.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FriendlyName
            });


            var oTicket = this.TicketService.GetTicket(id);
            var ticket = Mapper.Map<TicketViewModel>(oTicket);

            var functionalUnitList = oTicket.Consortium.Ownership.FunctionalUnits
                .Select(x => new SelectListItem()
            {
                    Value = x.Id.ToString(),
                    Text = oTicket.Consortium.Ownership.Address.Street + " " + oTicket.Consortium.Ownership.Address.Street + "-"
                    + "Nro:" + x.Number + " Piso:" + x.Floor + " Dto:" + x.Dto
                });


            ticket.StatusList = statusList;
            ticket.PriorityList = priorityList;
            ticket.WorkersList = workersList;
            ticket.ManagerList = managerList;
            ticket.ProviderList = providerList;
            ticket.UsersList = userList;
            ticket.ConsortiumList = consortiumList;
            ticket.FunctionalUnitList = functionalUnitList;
            ticket.Consortium = oTicket.Consortium;
            ticket.Worker = oTicket.WorkerId > 0 ? this.WorkerService.GetWorker(oTicket.WorkerId) : null;
            ticket.ConsortiumId = oTicket.Consortium != null ? oTicket.Consortium.Id : 0;
            ticket.FunctionalUnit = oTicket.FunctionalUnit;
            ticket.FunctionalUnitId = oTicket.FunctionalUnit != null ? oTicket.FunctionalUnit.Id : 0;
            ticket.SpendItemList = spendItemsList;            

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

        public ActionResult CloseTicket(int id)
        {
            var ticket = this.TicketService.GetTicket(id);
            var statusList = this.StatusService.GetAll();
            ticket.Status.Id = statusList.Where(x => x.Description.Equals("closed")).FirstOrDefault().Id;
            ticket.CloseDate = DateTime.Now;
            var nticket = Mapper.Map<TicketRequest>(ticket);
            this.TicketService.UpdateTicket(nticket);
            return Redirect("/Backlog/UpdateTicketById/"+id);
        }

        public ActionResult SetTicketBlocker(int id)
        {
            var ticket = this.TicketService.GetTicket(id);
            var statusList = this.PriorityService.GetAll();
            ticket.Priority.Id = statusList.Where(x => x.Description.Equals("bloqueante")).FirstOrDefault().Id;
            var nticket = Mapper.Map<TicketRequest>(ticket);
            this.TicketService.UpdateTicket(nticket);
            return Redirect("/Backlog/Index");
        }



    }
}