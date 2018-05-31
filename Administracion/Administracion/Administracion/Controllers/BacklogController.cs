using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.List;
using Administracion.Dto.Ticket;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.AreaService;
using Administracion.Services.Contracts.Autentication;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Managers;
using Administracion.Services.Contracts.Messages;
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
        public virtual IAreaService AreaService { get; set; }
        public virtual IMessageService MessageService { get; set; }


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

            var priorityList = this.PriorityService.GetAll().Where(x => x.Description != "bloqueante").Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var areaList = this.AreaService.GetAll().Select(x => new SelectListItem()
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

            var backloguserList = this.  ManagerService.GetAll().Select(x => new SelectListItem()
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
                AreaList = areaList,
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

            if (ticket.Autoasign)
            {
                nticket.BacklogUserId = SessionPersister.Account.Id;
            }
            else
            {
                if (nticket.Id > 0)
                {
                    var oticket = this.TicketService.GetTicket(nticket.Id);
                    if(oticket.BacklogUser!= null && oticket.BacklogUser.Id == SessionPersister.Account.Id)
                    {
                        nticket.BacklogUserId = null;
                    }
                }

            }

            try
            {
                var result = false;
                if (nticket.Id == 0)
                {
                    result = this.TicketService.CreateTicket(nticket);
                }
                else
                {
                    var oTicket = this.TicketService.GetTicket(ticket.Id);
                    if ((oTicket.Manager != null && nticket.ManagerId != oTicket.Manager.Id) )
                    {
                        var content = "Reasigne el ticket a:";
                        if (nticket.ManagerId.HasValue && nticket.ManagerId.Value > 0)
                        {
                            var manager = this.ManagerService.GetManager(nticket.ManagerId.Value);
                            content += manager.User.Name + " " + manager.User.Surname;
                        }
                        else
                        {
                            content += "nadie";
                        }

                        
                        this.MessageService.CreateMessage(new Dto.Message.MessageRequest()
                        {
                            Content = content,
                            Date = DateTime.Now,
                            SenderId = SessionPersister.Account.User.Id,
                            TicketId = ticket.Id
                        });

                    }

                    if((oTicket.BacklogUser != null && nticket.BacklogUserId != oTicket.BacklogUser.Id))
                    {
                        var content = "Reasigne el ticket a:";
                        if (nticket.BacklogUserId.HasValue && nticket.BacklogUserId.Value > 0)
                        {
                            content += "mi";
                        }
                        else
                        {
                            content += "nadie";
                        }

                        this.MessageService.CreateMessage(new Dto.Message.MessageRequest()
                        {
                            Content = content,
                            Date = DateTime.Now,
                            SenderId = SessionPersister.Account.User.Id,
                            TicketId = ticket.Id
                        });


                    }

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

        [HttpPost]
        public ActionResult CreateTicketFollow(TicketHistoryViewModel ticket)
        {
            try
            {
                var ticketHistoryRequest = Mapper.Map<TicketHistoryRequest>(ticket);
                this.TicketService.CreateTicketHistory(ticketHistoryRequest);
                return Redirect(string.Format("/Backlog/UpdateTicketById/{0}", ticket.TicketId));
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

            var areaList = this.AreaService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var priorityList = this.PriorityService.GetAll().Where(x => x.Description != "bloqueante").Select(x => new SelectListItem()
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

            var back_user = this.AuthenticationService.GetAll().Where(x => x.Id == ticket.Creator.Id).FirstOrDefault();
            var user = this.UserService.GetUser(back_user.User.Id);

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
            ticket.Manager = oTicket.Manager;
            ticket.ConsortiumId = oTicket.Consortium != null ? oTicket.Consortium.Id : 0;
            ticket.FunctionalUnit = oTicket.FunctionalUnit;
            ticket.FunctionalUnitId = oTicket.FunctionalUnit != null ? oTicket.FunctionalUnit.Id : 0;
            ticket.SpendItemList = spendItemsList;
            ticket.Creator = user;
            ticket.AreaList = areaList;
            ticket.Area = oTicket.Area;
            ticket.Autoasign = oTicket.BacklogUser != null && oTicket.BacklogUser.Id == SessionPersister.Account.Id;
            ticket.BacklogUser = oTicket.BacklogUser;

            return View("CreateTicket",ticket);
        }

        public ActionResult UpdateTicket(TicketViewModel ticket)
        {
            var oTicket = this.TicketService.GetTicket(ticket.Id);
            var nticket = Mapper.Map<TicketRequest>(ticket);

            if ((oTicket.Manager != null && nticket.ManagerId != oTicket.Manager.Id))
            {
                var content = "Reasigné el ticket a:";
                if (nticket.ManagerId.HasValue && nticket.ManagerId.Value > 0)
                {
                    var manager = this.ManagerService.GetManager(nticket.ManagerId.Value);
                    content += manager.User.Name + " " + manager.User.Surname;
                }
                else
                {
                    content += "nadie";
                }

                this.MessageService.CreateMessage(new Dto.Message.MessageRequest()
                {
                    Content = content,
                    Date = DateTime.Now,
                    SenderId = SessionPersister.Account.User.Id,
                    TicketId = ticket.Id
                });

            }

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

            this.MessageService.CreateMessage(new Dto.Message.MessageRequest()
            {
                Content = "Cerré el ticket",
                Date = DateTime.Now,
                SenderId = SessionPersister.Account.User.Id,
                TicketId = id
            });
            
            return Redirect("/Backlog/UpdateTicketById/"+id);
        }

        public ActionResult ResolveTicket(int id)
        {
            var ticket = this.TicketService.GetTicket(id);
            var statusList = this.StatusService.GetAll();
            ticket.Status.Id = statusList.Where(x => x.Description.Equals("closed")).FirstOrDefault().Id;
            ticket.Resolved = true;
            ticket.CloseDate = DateTime.Now;
            
            var nticket = Mapper.Map<TicketRequest>(ticket);
            this.TicketService.UpdateTicket(nticket);

            this.MessageService.CreateMessage(new Dto.Message.MessageRequest()
            {
                Content = "Resolvi el ticket",
                Date = DateTime.Now,
                SenderId = SessionPersister.Account.User.Id,
                TicketId = id
            });

            return Redirect("/Backlog/UpdateTicketById/" + id);
        }

        public ActionResult SetTicketBlocker(int id)
        {
            var ticket = this.TicketService.GetTicket(id);
            var statusList = this.PriorityService.GetAll();
            ticket.Priority.Id = statusList.Where(x => x.Description.Equals("bloqueante")).FirstOrDefault().Id;
            var nticket = Mapper.Map<TicketRequest>(ticket);
            this.TicketService.UpdateTicket(nticket);

            this.MessageService.CreateMessage(new Dto.Message.MessageRequest()
            {
                Content = "Marqué el ticket como bloqueante",
                Date = DateTime.Now,
                SenderId = SessionPersister.Account.User.Id,
                TicketId = id
            });

            return Redirect("/Backlog/Index");
        }

        public ActionResult GetByStatus(string statusDescription)
        {
            try
            {
                var tickets = this.TicketService.GetAll()
                    .Where(x => x.Status.Description == statusDescription)
                    .ToList();

                var ticketsViewModel = Mapper.Map<List<TicketViewModel>>(tickets);

                return View("List", ticketsViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult GetByPriority(string priorityDescription)
        {
            try
            {
                var tickets = this.TicketService.GetAll()
                    .Where(x => x.Priority.Description == priorityDescription)
                    .ToList();

                var ticketsViewModel = Mapper.Map<List<TicketViewModel>>(tickets);

                return View("List", ticketsViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

    }
}