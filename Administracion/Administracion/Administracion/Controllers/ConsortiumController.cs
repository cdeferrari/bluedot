using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.Consortium;
using Administracion.Dto.List;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.Lists;
using Administracion.Services.Contracts.Owners;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.Renters;
using Administracion.Services.Contracts.Status;
using Administracion.Services.Implementations.Consortiums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    
    [CustomAuthorize(Roles.Root)]
    public class ConsortiumController : Controller
    {
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IAdministrationService AdministrationService { get; set; }
        public virtual IOwnershipService OwnershipService { get; set; }
        public virtual IStatusService StatusService { get; set; }
        public virtual IChecklistService ChecklistService { get; set; }
        public virtual IOwnerService OwnersService { get; set; }
        public virtual IRenterService RenterService { get; set; }
        // GET: Backlog
        public ActionResult Index()
        {
            try
            {
                var consortiums = this.ConsortiumService.GetAll();
                var consortiumsViewModel = Mapper.Map<List<ConsortiumViewModel>>(consortiums);
                return View("List", consortiumsViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

        [HttpGet]
        public ActionResult CreateConsortium()
        {
            var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var ownerships = this.OwnershipService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Address.Street + " " + x.Address.Number.ToString()
            });

            var viewModel = new ConsortiumViewModel()
            {
                Administrations = new SelectList(administrations, "Value", "Text"),
                Ownerships = new SelectList(ownerships, "Value", "Text"),
            };
            
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult CreateChecklist(int id)
        {
            var consortium = this.ConsortiumService.GetConsortium(id);
            var checklistvm = new CheckListViewModel()
            {
                Customer = consortium.FriendlyName,
                ConsortiumId = consortium.Id
            };
            checklistvm.Tasks = new List<TaskListViewModel>();
            var items = this.ChecklistService.GetItems();
            var TaskResults = new List<SelectListItem>()
            {
                new SelectListItem() { Value = 1.ToString(), Text = "Ok" },
                new SelectListItem() { Value = 2.ToString(), Text = "Error" },
                new SelectListItem() { Value = 3.ToString(), Text = "Indefinido" }
            };

            var statusList = this.StatusService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            foreach (var item in items)
            {
                checklistvm.Tasks.Add(
                    new TaskListViewModel()
                    {
                        Results = TaskResults,
                        StatusList = statusList,
                        Description = item.Description
                    });
            }
            
            return View(checklistvm);
        }


        [HttpGet]
        public ActionResult UpdateChecklist(int id)
        {
            var checklist = this.ChecklistService.GetList(id);

            var checklistvm = Mapper.Map<CheckListViewModel>(checklist);
            for (int i = 0; i < checklist.Tasks.Count; i++)
            {
                if (checklist.Tasks[i].Result.Description.Equals("success"))
                {
                    checklistvm.Tasks[i].IsSuccess = true;
                }

            }
            
            return View(checklistvm);
        }
        

        [HttpPost]
        public ActionResult CreateUpdateChecklist(CheckListViewModel checklist)
        {

            var nlist = new ListRequest()
            {
                Coments = checklist.Coments,
                ConsortiumId = checklist.ConsortiumId,
                Customer = checklist.Customer,
                Tasks = new List<TaskListRequest>(),
                OpenDate = DateTime.Now,
                Id = checklist.Id
            };

            var statusList = this.StatusService.GetAll();

            foreach (var task in checklist.Tasks)
            {
                var ntask = new TaskListRequest()
                {
                    Id= task.Id,
                    Coments = task.Coments,
                    Description = task.Description,
                    ResultId = task.IsSuccess ? 1 : 2,
                    StatusId = !task.IsSuccess ? statusList.Where(x => x.Description.Equals("open")).FirstOrDefault().Id 
                    : statusList.Where(x => x.Description.Equals("closed")).FirstOrDefault().Id

                };
                nlist.Tasks.Add(ntask);
            }
          

            try
            {
                var result = false;
                if (nlist.Id == 0)
                {
                    result = this.ChecklistService.CreateList(nlist);
                }
                else
                {
                    result = this.ChecklistService.UpdateList(nlist);
                }
                if (result)
                {
                    return Redirect("/Consortium/Index");
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
        public ActionResult CreateUpdateConsortium(ConsortiumViewModel consortium)
        {

            var nConsortium = Mapper.Map<ConsortiumRequest>(consortium);
            try
            {
                
                var result = false;
                if (nConsortium.Id == 0)
                {
                    var nOwnershp = Mapper.Map<Ownership>(consortium.Ownership);
                    var nresult = this.OwnershipService.CreateOwnership(nOwnershp);
                    if (nresult.Id > 0)
                    {
                        nConsortium.OwnershipId = nresult.Id;
                        nConsortium.AdministrationId = 1;
                        result = this.ConsortiumService.CreateConsortium(nConsortium);

                    }


                }
                else
                {
                    result = this.ConsortiumService.UpdateConsortium(nConsortium);
                }
                if (result)
                {
                    return Redirect("/Consortium/Index");
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

        [HttpGet]
        public ActionResult Details(int id)
        {
            var oConsortium = this.ConsortiumService.GetConsortium(id);            
          
            var consortium = Mapper.Map<ConsortiumDetailsViewModel>(oConsortium);

            var owners = this.OwnersService.GetAll();

            var renters = this.RenterService.GetAll();


            consortium.Checklists = this.ChecklistService
                .GetAll().Where(x => x.ConsortiumId.Equals(id)).OrderByDescending(x => x.OpenDate).Take(10).ToList();

            consortium.Ownership.FunctionalUnits.ForEach(x =>
            x.Owner = owners.Where(y => y.FunctionalUnitId.Equals(x.Id)).FirstOrDefault()
            );

            consortium.Ownership.FunctionalUnits.ForEach(x =>
            x.Renter = renters.Where(y => y.FunctionalUnitId.Equals(x.Id)).FirstOrDefault()
            );

            return View(consortium);
        }


        [HttpGet]
        public ActionResult UpdateConsortium(int id)
        {
            var oConsortium = this.ConsortiumService.GetConsortium(id);
            var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var ownerships = this.OwnershipService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Address.Street + " " + x.Address.Number.ToString()
            });
            

            var consortium = Mapper.Map<ConsortiumViewModel>(oConsortium);
            consortium.Administrations =  new SelectList(administrations, "Value", "Text");
            consortium.Ownerships = new SelectList(ownerships, "Value", "Text");

            return View("CreateConsortium",consortium);
        }

        [HttpPost]
        public ActionResult UpdateConsortium(ConsortiumViewModel consortium)
        {            
            
            var nConsortium = Mapper.Map<ConsortiumRequest>(consortium);            
            this.ConsortiumService.UpdateConsortium(nConsortium);
            return View();
        }

        public ActionResult DeleteConsortium(int id)
        {                    
            this.ConsortiumService.DeleteConsortium(id);
            return Redirect("/Consortium/Index");
            
        }
    }
}