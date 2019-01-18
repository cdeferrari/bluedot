using Administracion.DomainModel;
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
using Administracion.Services.Contracts.Provinces;
using Administracion.Dto.Manager;
using Administracion.Services.Contracts.ManagerPositions;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.All)]
    public class ManagersController : Controller
    {
        public virtual IManagerService ManagerService { get; set; }
        public virtual IUserService UserService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IManagerPositionService ManagerPositionService { get; set; }
        public virtual ILaboralUnionService LaboralUnionService { get; set; }
        public virtual IMultimediaService MultimediaService { get; set; }
        public virtual IProvinceService ProvinceService { get; set; }        

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
        public ActionResult CreateManager(int? id)
        {

            var consortium = id.HasValue ? this.ConsortiumService.GetConsortium(id.Value) : null;

            var consortiumList = this.ConsortiumService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FriendlyName
            });

            var managerPositionList = this.ManagerPositionService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var laboralUnions = this.LaboralUnionService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var provinces = this.ProvinceService.GetAllProvinces().Select(x => new SelectListItem()
            {
                Value = x.Description,
                Text = x.Description
            });

            var viewModel = new ManagerViewModel()
            {
                ConsortiumId = consortium != null ? consortium.Id : 0,
                LaboralUnionList = laboralUnions,
                ConsortiumList = consortiumList,
                ManagerPositionList = managerPositionList,
                Provinces = provinces
            };

            viewModel.Male = true;
            viewModel.StartDate = DateTime.Now;
            viewModel.BirthDate = DateTime.Now;
            return View(viewModel);            
        }

        [HttpPost]
        public ActionResult CreateUpdateManager(ManagerViewModel manager)
        {
            
            var consortium = manager.ConsortiumId != 0 ? this.ConsortiumService.GetConsortium(manager.ConsortiumId):null;            
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
                        this.UserService.UpdateUser(nuser);
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
                    nuser = Mapper.Map<User>(manager.User);
                    result = this.UserService.UpdateUser(nuser);
                    if(result)
                        result = this.ManagerService.UpdateManager(nmanager);
                }
                
                if (result)
                {
                    if (consortium != null)
                    {
                        return Redirect(string.Format("/Consortium/Details/{0}", consortium.Id));
                    }
                    else
                    {
                        return Redirect("/Managers/Index");
                    }
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


        public ActionResult UpdateManagerById(int id, int? consortiumId)
        {
            var consortium = consortiumId.HasValue ? this.ConsortiumService.GetConsortium(consortiumId.Value) : null;

            var provinces = this.ProvinceService.GetAllProvinces().Select(x => new SelectListItem()
            {
                Value = x.Description,
                Text = x.Description
            });

            var managerPositionList = this.ManagerPositionService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var omanager = this.ManagerService.GetManager(id);
            var laboralUnions = this.LaboralUnionService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var consortiumList = this.ConsortiumService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FriendlyName
            });


            var manager = Mapper.Map<ManagerViewModel>(omanager);
            manager.ConsortiumId =  consortiumId.HasValue ? consortiumId.Value : 0;
            manager.ConsortiumList = consortiumList;
            manager.LaboralUnionList = laboralUnions;
            manager.ManagerPositionList = managerPositionList;
            manager.Provinces = provinces;
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
