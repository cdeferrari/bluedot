using Administracion.DomainModel;
using Administracion.Dto.Owner;
using Administracion.Dto.Provider;
using Administracion.Dto.Renter;
using Administracion.Dto.Worker;
using Administracion.Models;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Owners;
using Administracion.Services.Contracts.Providers;
using Administracion.Services.Contracts.Renters;
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
using WebGrease.Css.Extensions;

namespace Administracion.Controllers
{
    public class WorkersController : Controller
    {
        public virtual IUserService UserService { get; set; }
        public virtual IFunctionalUnitService FunctionalUnitService { get; set; }
        public virtual IWorkerService WorkerService { get; set; }
        public virtual IOwnerService OwnerService { get; set; }
        public virtual IRenterService RenterService { get; set; }
        public virtual IProviderService ProviderService { get; set; }
        public virtual IAdministrationService AdministrationService { get; set; }

        public ActionResult Index()
        {
            try
            {
                var workers = this.WorkerService.GetAll();
                var usersIds = workers.Select(x => x.User.Id)            
                    .ToList();

                var users = this.UserService.GetAll().Select(x => !usersIds.Contains(x.Id)).ToList();


                var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });

                var paymentTypes = new List<SelectListItem>() { new SelectListItem() { Value = 1.ToString(), Text = "tarjeta de credito" } };
                var usersViewModel = new List<UserViewModel>();

                workers.ForEach(x => usersViewModel.Add(
                    new UserViewModel()
                    {
                        Id = x.User.Id,
                        CUIT = x.User.CUIT,
                        ContactData = Mapper.Map<ContactDataViewModel>(x.User.ContactData),
                        DNI = x.User.DNI,
                        Name = x.User.Name,
                        Surname = x.User.Surname,
                        IsWorker = true, 
                        Administrations = administrations,
                        PaymentTypes = paymentTypes
                    }));

                
                
                return View("List",usersViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateWorker()
        {
            var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            

            var functionalUnitList = this.FunctionalUnitService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Ownership.Address.Street+" "+x.Ownership.Address.Number+" - Piso:"+ x.Floor+" Unidad:"+ x.Dto
            });


            var userViewModel = new WorkerViewModel() { Administrations = administrations ,  FunctionalUnitList = functionalUnitList};
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateUser(UserViewModel user)
        {
         
            var nuser = new User();

            try
            {
                nuser = Mapper.Map<User>(user);

                if (user.Id == 0)
                {
                    nuser = this.UserService.CreateUser(nuser);

                    if (user.IsOwner)
                    {
                        var owner = new OwnerRequest()
                        {
                            UserId = nuser.Id,
                            FunctionalUnitId = user.FunctionalUnitId
                        };
                        this.OwnerService.CreateOwner(owner);
                    }

                    if (user.IsProvider)
                    {
                        var provider = new ProviderRequest()
                        {
                            UserId = nuser.Id
                        };
                        this.ProviderService.CreateProvider(provider);
                    }

                    if (user.IsRenter)
                    {
                        var renter = new RenterRequest()
                        {
                            UserId = nuser.Id,
                            PaymentTypeId = user.PaymentTypeId,
                            FunctionalUnitId = user.FunctionalUnitId
                        };
                        this.RenterService.CreateRenter(renter);
                    }

                    if (user.IsWorker)
                    {
                        var worker = new WorkerRequest()
                        {
                            UserId = nuser.Id,
                            AdministrationId = user.AdministrationId
                        };
                        this.WorkerService.CreateWorker(worker);
                    }

                
                }
                else
                {
                    this.UserService.UpdateUser(nuser);
                }
                return Redirect("/Users/Index");
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


        public ActionResult UpdateUserById(int id)
        {
            var oUser = this.UserService.GetUser(id);
            var user = Mapper.Map<UserViewModel>(oUser);
            var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var functionalUnitList = this.FunctionalUnitService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Ownership.Address.Street + " " + x.Ownership.Address.Number + " - Piso:" + x.Floor + " Unidad:" + x.Dto
            });

            user.FunctionalUnitList = functionalUnitList;
            user.Administrations = administrations;
            user.PaymentTypes = new List<SelectListItem>() { new SelectListItem() { Value = 1.ToString(), Text = "tarjeta de credito" } };
            return View("CreateUser",user);
        }

        public ActionResult UpdateUser(UserViewModel user)
        {            
            var nuser = new User();
            
            nuser = Mapper.Map<User>(user);            
            this.UserService.UpdateUser(nuser);
            return View();
        }

        public ActionResult DeleteUser(int id)
        {
            var workers = this.WorkerService.GetAll();
            var owners = this.OwnerService.GetAll();
            var renters = this.RenterService.GetAll();

            var workerUsers = workers.Select(x => x.User.Id).ToList();
            var ownerUsers = owners.Select(x => x.User.Id).ToList();
            var renterUsers = renters.Select(x => x.User.Id).ToList();

            if (workerUsers.Contains(id))
            {
                var worker = workers.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                this.WorkerService.DeleteWorker(worker.Id);
            }

            if (ownerUsers.Contains(id))
            {
                var owner = owners.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                this.OwnerService.DeleteOwner(owner.Id);
            }

            if (renterUsers.Contains(id))
            {
                var renter = renters.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                this.RenterService.DeleteRenter(renter.Id);
            }

            this.UserService.DeleteUser(id);
            return Redirect("/Users/Index");
        }
        
        public ActionResult List()
        {         
         
            try
            {
                var users = this.UserService.GetAll();
                return View(users);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


    }
}