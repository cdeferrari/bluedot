﻿using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.Managers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administracion.Services.Contracts.LaboralUnion;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Dto.Manager;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.Root)]
    public class ManagersController : Controller
    {
        public virtual IManagerService ManagerService { get; set; }
        public virtual IUserService UserService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual ILaboralUnionService LaboralUnionService { get; set; }
        public virtual IMultimediaService MultimediaService { get; set; }

        

        public ActionResult Index()
        {
            try
            {
                var managers = this.ManagerService.GetAll();
                var ManagerViewModel = Mapper.Map<List<ManagerViewModel>>(managers);
                return View("List", ManagerViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateManager(int id)
        {
            var consortium = this.ConsortiumService.GetConsortium(id);
            

            var laboralUnions = this.LaboralUnionService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var viewModel = new ManagerViewModel()
            {
                ConsortiumId = consortium.Id,
                LaboralUnionList = laboralUnions
               
            };

            return View(viewModel);            
        }

        [HttpPost]
        public ActionResult CreateUpdateManager(ManagerViewModel manager)
        {
         
            var consortium = this.ConsortiumService.GetConsortium(manager.ConsortiumId);            
            var nmanager = Mapper.Map<ManagerRequest>(manager);                        

            try
            {
                var nuser = new User();

                var result = false;
                if (nmanager.Id == 0)
                {   

                    if(manager.User.Id==0)
                    {
                        nuser = this.UserService.CreateUser(Mapper.Map<User>(manager.User));
                    }
                    else
                    {
                        nuser = Mapper.Map<User>(manager.User);
                    }

                    if (nuser.Id > 0)
                    {
                        nmanager.UserId = nuser.Id;
                        var entity = this.ManagerService.CreateManager(nmanager);
                        result = entity.Id != 0;
                    }

                }
                else
                {
                    result = this.ManagerService.UpdateManager(nmanager);
                }
                
                if (result)
                {
                    return Redirect(string.Format( "/Consortium/Details/{0}",consortium.Id));
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


        public ActionResult UpdateManagerById(int id, int consortiumId)
        {
            var omanager = this.ManagerService.GetManager(id);
            var laboralUnions = this.LaboralUnionService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var manager = Mapper.Map<ManagerViewModel>(omanager);
            manager.ConsortiumId = consortiumId;
            manager.LaboralUnionList = laboralUnions;
            return View("CreateManager",manager);
        }

        public ActionResult UpdateManager(ManagerViewModel manager)
        {       
            var nmanager = Mapper.Map<ManagerRequest>(manager);            
            this.ManagerService.UpdateManager(nmanager);
            return View();
        }

        public ActionResult DeleteManager(int id, int consortiumId)
        {                    
            var manager = this.ManagerService.GetManager(id);

            this.ManagerService.DeleteManager(id);

            this.UserService.DeleteUser(manager.User.Id);

            return Redirect(string.Format("/Consortium/Details/{0}", consortiumId));
        }
        
        public ActionResult List()
        {         
         
            try
            {
                var managers = this.ManagerService.GetAll();
                return View(managers);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }

    }
}
