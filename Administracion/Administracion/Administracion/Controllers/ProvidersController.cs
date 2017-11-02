using Administracion.DomainModel;
using Administracion.Dto.Owner;
using Administracion.Dto.Provider;
using Administracion.Dto.Renter;
using Administracion.Dto.Worker;
using Administracion.Models;
using Administracion.Services.Contracts.Administrations;
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
    public class ProvidersController : Controller
    {
        public virtual IUserService UserService { get; set; }
        public virtual IProviderService ProviderService { get; set; }
        public virtual IAdministrationService AdministrationService { get; set; }

        public ActionResult Index()
        {
            try
            {        
                var providers = this.ProviderService.GetAll();

                var usersViewModel = Mapper.Map<List<ProviderViewModel>>(providers);
                var usersIds = providers.Select(x => x.User.Id)
                    .ToList();
                
                
                return View("List",usersViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateProvider()
        {
            var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            
            var userViewModel = new ProviderViewModel() { Administrations = administrations };
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateProvider(ProviderViewModel provider)
        {
         
            var nuser = new User();

            try
            {
                nuser = Mapper.Map<User>(provider.User);
                nuser.Surname = provider.User.Name;


                if (provider.Id == 0)
                {

                    nuser = this.UserService.CreateUser(nuser);

                    var nprovider = new ProviderRequest()
                    {
                        UserId = nuser.Id,
                        Item = provider.Item,
                        Address = Mapper.Map<Address>(provider.Address)
                    };

                    this.ProviderService.CreateProvider(nprovider);
                }
                else
                {
                    this.UserService.UpdateUser(nuser);
                    var nprovider = Mapper.Map<Provider>(provider);

                    this.ProviderService.UpdateProvider(
                        new ProviderRequest()
                        {
                            Address = nprovider.Address,
                            Id = nprovider.Id,
                            Item = nprovider.Item,
                            UserId = nprovider.User.Id                        
                        });
                }

                return Redirect("/Providers/Index");
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


        public ActionResult UpdateProviderById(int id)
        {
            var oUser = this.ProviderService.GetProvider(id);
            var user = Mapper.Map<ProviderViewModel>(oUser);
            var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            user.Administrations = administrations;
            return View("CreateProvider",user);
        }

        public ActionResult UpdateUser(UserViewModel user)
        {            
            var nuser = new User();
            
            nuser = Mapper.Map<User>(user);            
            this.UserService.UpdateUser(nuser);
            return View();
        }

        public ActionResult DeleteProvider(int id)
        {                    
            this.ProviderService.DeleteProvider(id);
            return Redirect("/Providers/Index");
        }
        
        public ActionResult List()
        {         
         
            try
            {
                var users = this.ProviderService.GetAll();
                return View(users);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


    }
}