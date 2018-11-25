using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.CommonData;
using Administracion.Dto.Consortium;
using Administracion.Dto.ConsortiumConfigurations;
using Administracion.Dto.Control;
using Administracion.Dto.List;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.CommonData;
using Administracion.Services.Contracts.ConsortiumConfigurations;
using Administracion.Services.Contracts.ConsortiumConfigurationTypes;
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
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop.Excel;
using Administracion.Services.Contracts.UnitConfigurationTypes;
using Administracion.Dto.UnitConfigurations;
using Administracion.Services.Contracts.UnitConfigurations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;
using System.Globalization;

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

        public virtual IConsortiumConfigurationService ConsortiumConfigurationService { get; set; }
        public virtual IUnitConfigurationService UnitConfigurationService { get; set; }
        public virtual IConsortiumConfigurationTypeService ConsortiumConfigurationTypeService { get; set; }
        public virtual IUnitConfigurationTypeService UnitConfigurationTypeService { get; set; }
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

            

            IList<Province> provinceList = this.CountryService.GetAllProvinces(int.Parse(ConfigurationManager.AppSettings["default_country_id"]));

            Province provincia = provinceList.FirstOrDefault();
            List<City> cityList = provincia == null ? new List<City>() : provincia.Cities.ToList();

            ConsortiumViewModel viewModel = new ConsortiumViewModel()
            {
                Administrations = new SelectList(administrations, "Value", "Text"),
                Ownerships = new SelectList(ownerships, "Value", "Text"),
                CommonDataItems = new SelectList(commonDataItems, "Value", "Text"),
                Provinces = new SelectList(provinceList, "Description", "Description"),
                Cities = new SelectList(cityList, "Description", "Description")
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
        public ActionResult CreateUpdateConsortiumConfiguration(int id)
        {

            var configurationTypes = this.ConsortiumConfigurationTypeService.GetAll();
            var configurations = this.ConsortiumConfigurationService.GetByConsortiumId(id, DateTime.Now.AddYears(-1), DateTime.Now);
            var configDictionary = new Dictionary<int, ConsortiumConfiguration>();

            foreach(var conft in configurationTypes)
            {
                var lastConfig = configurations.Where(x => x.Type.Id == conft.Id)
                    .OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();
                if(lastConfig != null)
                {
                    configDictionary.Add(conft.Id, lastConfig);
                }
            }

            var ConfigurationVm = new ConsortiumConfigurationViewModel()
            {
                Configurations = configDictionary,
                ConfigurationTypes  = configurationTypes,
                ConsortiumId = id
            };

            return View(ConfigurationVm);
        }

        [HttpPost]
        public ActionResult CreateUpdateConsortiumConfiguration(ConsortiumConfigurationViewModel configurationModel)
        {
            var configurationTypes = this.ConsortiumConfigurationTypeService.GetAll();
            var configurations = this.ConsortiumConfigurationService.GetByConsortiumId(configurationModel.ConsortiumId, DateTime.Now.AddYears(-1), DateTime.Now);
            var configDictionary = new Dictionary<int, ConsortiumConfiguration>();

            foreach (var conft in configurationTypes)
            {
                var lastConfig = configurations.Where(x => x.Type.Id == conft.Id)
                    .OrderByDescending(x => x.ConfigurationDate).FirstOrDefault();
                if (lastConfig != null)
                {
                    configDictionary.Add(conft.Id, lastConfig);
                }
            }


            foreach (var configuration in configurationModel.ConsortiumConfigurations)
            {
                var actualConfig = configDictionary.ContainsKey(configuration.ConsortiumConfigurationTypeId) ?
                    configDictionary[configuration.ConsortiumConfigurationTypeId] : null;                    

                if (configuration.Value != 0 && (actualConfig == null || actualConfig.Value != configuration.Value))
                {
                    var confRequest = new ConsortiumConfigurationRequest()
                    {
                        ConsortiumConfigurationTypeId = configuration.ConsortiumConfigurationTypeId,
                        Value = configuration.Value,
                        ConsortiumId = configurationModel.ConsortiumId
                    };
                    this.ConsortiumConfigurationService.CreateConsortiumConfiguration(confRequest);
                }
            }

            return Redirect("/Consortium/CreateUpdateConsortiumConfiguration/" + configurationModel.ConsortiumId);
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

        [HttpGet]
        public ActionResult RegisterUnitsMonthDebt(int id)
        {
            this.ConsortiumService.RegisterUnitsMonthDebt(id);
            return Redirect(string.Format("/Consortium/Details/{0}", id));
        }

        [HttpGet]
        public ActionResult ConsortiumAccountStatusSummary(int id)
        {
            var unitsReport = this.ConsortiumService.GetConsortiumAccountStatusSummary(id);
            return View(unitsReport);
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

        

        public ActionResult ProcessConfigurationsCsv()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var unitConfigurationList = new List<UnitConfigurationRequest>();
                    var consortiums = this.ConsortiumService.GetAll();
                    var unitConfigurationTypes = this.UnitConfigurationTypeService.GetAll();

                    var gastosATypeId = unitConfigurationTypes.Where(x => x.Description == "gasto tipo A").FirstOrDefault().Id;
                    var gastosBTypeId = unitConfigurationTypes.Where(x => x.Description == "gasto tipo B").FirstOrDefault().Id;
                    var gastosCTypeId = unitConfigurationTypes.Where(x => x.Description == "gasto tipo C").FirstOrDefault().Id;
                    var gastosDTypeId = unitConfigurationTypes.Where(x => x.Description == "gasto tipo D").FirstOrDefault().Id;
                    var gastosExtraTypeId = unitConfigurationTypes.Where(x => x.Description == "expensas extraordinarias").FirstOrDefault().Id;
                    var gastosAysaTypeId = unitConfigurationTypes.Where(x => x.Description == "AYSA").FirstOrDefault().Id;
                    var gastosEdesurTypeId = unitConfigurationTypes.Where(x => x.Description == "EDESUR").FirstOrDefault().Id;
                    var provider = new CultureInfo("en-ES");

                    StreamReader csvreader = new StreamReader(file.InputStream);

                    var path = AppDomain.CurrentDomain.BaseDirectory + "/" + file.FileName;

                    while (!csvreader.EndOfStream)
                    {

                        var line = csvreader.ReadLine();
                        var values = line.Split(';').ToList();


                        var consortiumCuit = values[0];             
                        var consortium = consortiums.Where(x => x.CUIT == consortiumCuit).FirstOrDefault();

                        if (consortium != null)
                        {
                            var ufNumber = int.Parse(values[1]);                            
                            var unit = consortium.Ownership.FunctionalUnits.Where(x => x.Number == ufNumber).FirstOrDefault();
                            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
                            if (unit != null)
                            {

                                //decimal gastosAValue = 0;
                                //decimal gastosBValue = 0;
                                //decimal gastosCValue = 0;
                                //decimal gastosDValue = 0;
                                //decimal gastosExtraValue = 0;
                                //decimal gastosAYSAValue = 0;
                                //decimal gastosEDESURValue = 0;

                                //var addA = decimal.TryParse(values[3],out gastosAValue);
                                //gastosAValue = gastosAValue * 100;

                                //var addB = decimal.TryParse(values[4], out gastosBValue);
                                //gastosBValue = gastosBValue * 100;

                                //var addC = decimal.TryParse(values[5], out gastosCValue);
                                //gastosCValue = gastosCValue * 100;

                                //var addD = decimal.TryParse(values[6], out gastosDValue);
                                //gastosDValue = gastosDValue * 100;

                                //var addExtra = decimal.TryParse(values[7], out gastosExtraValue);
                                //gastosExtraValue = gastosExtraValue * 100;

                                //var addAYSA = decimal.TryParse(values[8], out gastosAYSAValue);
                                //gastosAYSAValue = gastosAYSAValue * 100;

                                //var addEDESUR = decimal.TryParse(values[9], out gastosEDESURValue);
                                //gastosEDESURValue = gastosEDESURValue * 100;

                                var gastosAValue = decimal.Parse(values[3], numberFormatInfo) * 100;
                                var gastosBValue = decimal.Parse(values[4], numberFormatInfo) * 100;
                                var gastosCValue = decimal.Parse(values[5], numberFormatInfo) * 100;
                                var gastosDValue = decimal.Parse(values[6], numberFormatInfo) * 100;
                                var gastosExtraValue = decimal.Parse(values[7], numberFormatInfo) * 100;
                                var gastosAYSAValue = decimal.Parse(values[8], numberFormatInfo) * 100;
                                var gastosEDESURValue = decimal.Parse(values[9], numberFormatInfo) * 100;


                                var configurationGastoA = new UnitConfigurationRequest()
                                {
                                    UnitId = unit.Id,
                                    Value = gastosAValue,
                                    UnitConfigurationTypeId = gastosATypeId
                                };

                                var configurationGastoB = new UnitConfigurationRequest()
                                {
                                    UnitId = unit.Id,
                                    Value = gastosBValue,
                                    UnitConfigurationTypeId = gastosBTypeId
                                };

                                var configurationGastoC = new UnitConfigurationRequest()
                                {
                                    UnitId = unit.Id,
                                    Value = gastosCValue,
                                    UnitConfigurationTypeId = gastosCTypeId
                                };

                                var configurationGastoD = new UnitConfigurationRequest()
                                {
                                    UnitId = unit.Id,
                                    Value = gastosDValue,
                                    UnitConfigurationTypeId = gastosDTypeId
                                };

                                var configurationGastoExtra = new UnitConfigurationRequest()
                                {
                                    UnitId = unit.Id,
                                    Value = gastosExtraValue,
                                    UnitConfigurationTypeId = gastosExtraTypeId
                                };

                                var configurationGastoAysa = new UnitConfigurationRequest()
                                {
                                    UnitId = unit.Id,
                                    Value = gastosAYSAValue,
                                    UnitConfigurationTypeId = gastosAysaTypeId
                                };

                                var configurationGastoEdesur = new UnitConfigurationRequest()
                                {
                                    UnitId = unit.Id,
                                    Value = gastosEDESURValue,
                                    UnitConfigurationTypeId = gastosEdesurTypeId
                                };

                                //if(addA) unitConfigurationList.Add(configurationGastoA);
                                //if (addB) unitConfigurationList.Add(configurationGastoB);
                                //if (addC) unitConfigurationList.Add(configurationGastoC);
                                //if (addD) unitConfigurationList.Add(configurationGastoD);
                                //if (addExtra) unitConfigurationList.Add(configurationGastoExtra);
                                //if (addAYSA) unitConfigurationList.Add(configurationGastoAysa);
                                //if (addEDESUR) unitConfigurationList.Add(configurationGastoEdesur);

                                unitConfigurationList.Add(configurationGastoA);
                                unitConfigurationList.Add(configurationGastoB);
                                unitConfigurationList.Add(configurationGastoC);
                                unitConfigurationList.Add(configurationGastoD);
                                unitConfigurationList.Add(configurationGastoExtra);
                                unitConfigurationList.Add(configurationGastoAysa);
                                unitConfigurationList.Add(configurationGastoEdesur);

                            }

                        }

                    }
                    
                    unitConfigurationList.ForEach(x => this.UnitConfigurationService.CreateUnitConfiguration(x));
                }
            }

            return Redirect(string.Format("/Consortium/Index"));
        }
        
        
        private static string GetContentXml(Stream fileStream)
        {
            var contentXml = "";

            using (var zipInputStream = new ZipInputStream(fileStream))
            {
                ZipEntry contentEntry = null;
                while ((contentEntry = zipInputStream.GetNextEntry()) != null)
                {
                    if (!contentEntry.IsFile)
                        continue;
                    if (contentEntry.Name.ToLower() == "content.xml")
                        break;
                }

                if (contentEntry.Name.ToLower() != "content.xml")
                {
                    throw new Exception("Cannot find content.xml");
                }

                var bytesResult = new byte[] { };
                var bytes = new byte[2000];
                var i = 0;

                while ((i = zipInputStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    var arrayLength = bytesResult.Length;
                    Array.Resize<byte>(ref bytesResult, arrayLength + i);
                    Array.Copy(bytes, 0, bytesResult, arrayLength, i);
                }
                contentXml = Encoding.UTF8.GetString(bytesResult);
            }

            return contentXml;
            
        }


    }
}