using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.CommonData;
using Administracion.Dto.Consortium;
using Administracion.Dto.Control;
using Administracion.Dto.List;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.CommonData;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.Countries;
using Administracion.Services.Contracts.ElevatorControlService;
using Administracion.Services.Contracts.FireExtinguisherControlService;
using Administracion.Services.Contracts.Lists;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.Owners;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.Providers;
using Administracion.Services.Contracts.Renters;
using Administracion.Services.Contracts.Status;
using Administracion.Services.Contracts.TaskResult;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Implementations.Consortiums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    
    [CustomAuthorize(Roles.All)]
    public class ConsortiumController : Controller
    {
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual ICountryService CountryService { get; set; }
        public virtual IAdministrationService AdministrationService { get; set; }
        public virtual IOwnershipService OwnershipService { get; set; }
        public virtual IStatusService StatusService { get; set; }
        public virtual ITaskResultService TaskResultService { get; set; }
        public virtual ITicketService TicketService { get; set; }
        public virtual ICommonDataService CommonDataService { get; set; }
        public virtual IChecklistService ChecklistService { get; set; }
        public virtual IOwnerService OwnersService { get; set; }
        public virtual IRenterService RenterService { get; set; }
        public virtual IProviderService ProviderService { get; set; }
        public virtual IMultimediaService MultimediaService { get; set; }
        public virtual IElevatorControlService ElevatorControlService { get; set; }
        public virtual IFireExtinguisherControlService FireExtinguisherControlService { get; set; }
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

            var commonDataItems = this.CommonDataService.GetItems().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            

            var provinces = this.CountryService
                .GetAllProvinces(int.Parse(ConfigurationManager.AppSettings["default_country_id"]));

            var provincesList = provinces.Select(x => new SelectListItem()
            {
                Value = x.Description,
                Text = x.Description
            });

            var cities = provinces.FirstOrDefault().Cities;

            var citiesList = cities.Select(x => new SelectListItem()
            {
                Value = x.Description,
                Text = x.Description
            });

            var viewModel = new ConsortiumViewModel()
            {
                Administrations = new SelectList(administrations, "Value", "Text"),
                Ownerships = new SelectList(ownerships, "Value", "Text"),
                CommonDataItems = new SelectList(commonDataItems, "Value", "Text"),
                Provinces = provincesList,
                Cities = citiesList
            };

            viewModel.Ownership = new OwnershipViewModel()
            {
                Address = new AddressViewModel()
                {
                    Province = "Buenos Aires",
                    City = "Ciudad Autónoma de Buenos Aires"
                }
            };
            
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult CreateChecklist(int id)
        {
            var consortium = this.ConsortiumService.GetConsortium(id);

            var customer = consortium.Managers.Count > 0 ?
                consortium.Managers.Where(x => !x.IsAlternate).FirstOrDefault() != null ?
                    consortium.Managers.Where(x => !x.IsAlternate).FirstOrDefault().User.Name
                    +" " +consortium.Managers.Where(x => !x.IsAlternate).FirstOrDefault().User.Surname :
                    consortium.Managers.Where(x => x.IsAlternate).FirstOrDefault().User.Name
                    + " " + consortium.Managers.Where(x => x.IsAlternate).FirstOrDefault().User.Surname
                : consortium.FriendlyName;
            var checklistvm = new CheckListViewModel()
            {
                Customer = customer,
                ConsortiumId = consortium.Id
            };
            checklistvm.Tasks = new List<TaskListViewModel>();
            var items = this.ChecklistService.GetItems();
            var TaskResults = this.TaskResultService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });
            
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
        public ActionResult FastUpdateConsortium(FastUpdateViewModel fastData)
        {
            var oConsortium = this.ConsortiumService.GetConsortium(fastData.Id);
            var result = true;
            if (fastData.Address != null)
            {
                var ownership = oConsortium.Ownership;
                ownership.Address.Street = fastData.Address.Street;
                ownership.Address.Number = fastData.Address.Number;
                result = this.OwnershipService.UpdateOwnership(ownership);
            }

            if (!string.IsNullOrEmpty(fastData.MailingList))
            {
                result = this.ConsortiumService.UpdateConsortium(new ConsortiumRequest()
                {
                    AdministrationId = oConsortium.Administration.Id,
                    CUIT = oConsortium.CUIT,
                    Telephone = oConsortium.Telephone,
                    OwnershipId = oConsortium.Ownership.Id,
                    FriendlyName = oConsortium.FriendlyName,
                    MailingList = fastData.MailingList,
                    Id = oConsortium.Id
                });
            }


            if (!string.IsNullOrEmpty(fastData.Telephone))
            {
                result = this.ConsortiumService.UpdateConsortium(new ConsortiumRequest()
                {
                    AdministrationId = oConsortium.Administration.Id,
                    CUIT = oConsortium.CUIT,
                    Telephone = fastData.Telephone,
                    OwnershipId = oConsortium.Ownership.Id,
                    FriendlyName = oConsortium.FriendlyName,
                    MailingList = oConsortium.MailingList,
                    Id = oConsortium.Id
                });
            }

            if (result)
            {
                return Redirect(string.Format("/Consortium/Details/{0}", fastData.Id));
            }
            else
            {
                return View("../Shared/Error");
            }
        }


        [HttpPost]
        public ActionResult CreateElevatorControl(ElevatorControlViewModel control)
        {
            var ncontrol = new ControlRequest()
            {
                ConsortiumId = control.ConsortiumId,
                ControlDate = DateTime.Now,
                ExpirationDate = control.ExpirationDate,
                ProviderId = control.ProviderId
            };

            try
            {
                var result = this.ElevatorControlService
                    .CreateElevatorControl(ncontrol);

                if (result)
                {
                    return Redirect(string.Format("/Consortium/Details/{0}", control.ConsortiumId));
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
        public ActionResult CreateFireExtinguisherControl(FireExtinguisherControlViewModel control)
        {
            var ncontrol = new ControlRequest()
            {
                ConsortiumId= control.ConsortiumId,
                ControlDate = DateTime.Now,
                ExpirationDate = control.ExpirationDate,
                ProviderId = control.ProviderId
            };
            
            try
            {
                var result = this.FireExtinguisherControlService
                    .CreateFireExtinguisherControl(ncontrol);
                
                if (result)
                {
                    return Redirect(string.Format("/Consortium/Details/{0}", control.ConsortiumId));
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
        public ActionResult CreateUpdateChecklist(CheckListViewModel checklist)
        {
            
            var nlist = new ListRequest()
            {
                Coments = checklist.Coments,
                ConsortiumId = checklist.ConsortiumId,
                Customer =  checklist.Customer,
                Tasks = new List<TaskListRequest>(),
                OpenDate = DateTime.Now,
                Id = checklist.Id
            };

            var statusList = this.StatusService.GetAll();
            var TaskResults = this.TaskResultService.GetAll();
            foreach (var task in checklist.Tasks)
            {
                var ntask = new TaskListRequest()
                {
                    Id= task.Id,
                    Coments = task.Coments,
                    Description = task.Description,
                    ResultId = task.IsSuccess ? TaskResults.Where(x => x.Description.Equals("success")).FirstOrDefault().Id 
                    : TaskResults.Where(x => x.Description.Equals("failed")).FirstOrDefault().Id,
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
                    return Redirect(string.Format("/Consortium/Details/{0}", checklist.ConsortiumId));
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
                
                bool result = false;
                if (nConsortium.Id == 0)
                {
                    var nOwnershp = Mapper.Map<Ownership>(consortium.Ownership);
                    var nresult = this.OwnershipService.CreateOwnership(nOwnershp);
                    if (nresult.Id > 0)
                    {                         
                        this.UploadMultimedia(nresult.Id);

                        nConsortium.OwnershipId = nresult.Id;
                        nConsortium.AdministrationId = 1;
                        result = this.ConsortiumService.CreateConsortium(nConsortium);
                        if (result)
                        {
                            foreach (var data in consortium.Ownership.CommonData)
                            {
                                var cdataRequest = new CommonDataRequest()
                                {
                                    CommonDataItemId = data.Item.Id,
                                    Have = data.Have,
                                    OwnershipId = nConsortium.OwnershipId
                                };
                                this.CommonDataService.CreateCommonData(cdataRequest);
                            }
                        }
                    }
                }
                else
                {
                    var oOwnership = this.OwnershipService.GetOwnership(consortium.Ownership.Id);
                    this.UploadMultimedia(consortium.Ownership.Id);
                    var nOwnership = new Ownership()
                    {
                        Address =  new Address()
                        {
                            City = consortium.Ownership.Address.City,
                            Number = consortium.Ownership.Address.Number,
                            PostalCode = consortium.Ownership.Address.PostalCode,
                            Province =  consortium.Ownership.Address.Province,
                            Street = consortium.Ownership.Address.Street
                        },
                        Category = consortium.Ownership.Category,
                        FunctionalUnits = oOwnership.FunctionalUnits,
                        Id = oOwnership.Id
                    };

                    result = this.OwnershipService.UpdateOwnership(nOwnership);
                    if (result)
                    {
                        
                        result = this.ConsortiumService.UpdateConsortium(nConsortium);
                        if (result)
                        {
                            foreach (var data in consortium.Ownership.CommonData)
                            {
                                if (data.Id == 0)
                                {
                                    var cdataRequest = new CommonDataRequest()
                                    {
                                        CommonDataItemId = data.Item.Id,
                                        Have = data.Have,
                                        OwnershipId = consortium.Ownership.Id
                                    };
                                    this.CommonDataService.CreateCommonData(cdataRequest);
                                }
                                else
                                {
                                    var cdataRequest = new CommonDataRequest()
                                    {
                                        Id = data.Id,
                                        CommonDataItemId = data.Item.Id,
                                        Have = data.Have,
                                        OwnershipId = consortium.Ownership.Id
                                    };
                                    this.CommonDataService.UpdateCommonData(cdataRequest);
                                }

                            }
                        }
                    }
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

            var tickets = this.TicketService.GetByConsortiumId(id);

            List<Provider> providerList = this.ProviderService.GetAll().ToList();
            providerList.Sort((x, y) => string.Compare(x.User.Name, y.User.Name));

            consortium.Providers = new SelectList(providerList, "Id", "User.Name");

            consortium.TicketQuantity = tickets.Where(x => x.Status.Description.ToLower().Equals("open")).Count();

            consortium.Checklists = this.ChecklistService
                .GetAll().Where(x => x.ConsortiumId.Equals(id))
                .OrderByDescending(x => x.OpenDate).Take(10).ToList();
            //consortium.Checklists = this.ConsortiumService
            //    .GetAllChecklists(id).OrderByDescending(x => x.OpenDate).Take(10).ToList();
            
            consortium.Ownership.FunctionalUnits.ForEach(x =>
            x.Owner = owners.Where(y => y.FunctionalUnitId.Contains(x.Id)).FirstOrDefault()
            );

            consortium.Ownership.FunctionalUnits.ForEach(x =>
            x.Renter = renters.Where(y => y.FunctionalUnitId.Equals(x.Id)).FirstOrDefault()
            );

            consortium.ImageUrl = oConsortium.Ownership.Multimedia != null && oConsortium.Ownership.Multimedia.Count > 0 ?
             ConfigurationManager.AppSettings["ImagePath"] + oConsortium.Ownership.Multimedia.OrderByDescending(x => x.Id).FirstOrDefault().Url : ConfigurationManager.AppSettings["ImagePath"] + "nophoto.jpg";

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

            var commonDataItems = this.CommonDataService.GetItems().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var provinces = this.CountryService
                .GetAllProvinces(int.Parse(ConfigurationManager.AppSettings["default_country_id"]));

            var provincesList = provinces.Select(x => new SelectListItem()
            {
                Value = x.Description,
                Text = x.Description
            });

            var cities = provinces.FirstOrDefault().Cities;

            var citiesList = cities.Select(x => new SelectListItem()
            {
                Value = x.Description,
                Text = x.Description
            });


            var consortium = Mapper.Map<ConsortiumViewModel>(oConsortium);
            consortium.Provinces = provincesList;
            consortium.Cities = citiesList;
            consortium.Administrations =  new SelectList(administrations, "Value", "Text");
            consortium.Ownerships = new SelectList(ownerships, "Value", "Text");
            consortium.CommonDataItems = new SelectList(commonDataItems, "Value", "Text");

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

        private void UploadMultimedia(int ownershipId)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                    this.MultimediaService.CreateMultimedia(new Multimedia()
                    {
                        MultimediaTypeId = (int)TipoMultimedia.Foto,
                        Url = fileName,
                        OwnershipId = ownershipId
                    });
                }
            }
        }
    }
}