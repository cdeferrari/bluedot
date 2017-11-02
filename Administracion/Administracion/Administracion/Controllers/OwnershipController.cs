using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.Ownerships;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.Root)]
    public class OwnershipController : Controller
    {
        public virtual IOwnershipService OwnershipService { get; set; }
        public virtual IMultimediaService MultimediaService { get; set; }

        

        public ActionResult Index()
        {
            try
            {
                var owners = this.OwnershipService.GetAll();
                var ownershipViewModel = Mapper.Map<List<OwnershipViewModel>>(owners);
                return View("List", ownershipViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateOwnership()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUpdateOwnership(OwnershipViewModel owner)
        {
         
            var nowner = Mapper.Map<Ownership>(owner);            

            try
            {
                var result = false;
                if (nowner.Id == 0)
                {                    
                    var entity = this.OwnershipService.CreateOwnership(nowner);
                    result = entity.Id != 0;

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
                                OwnershipId = entity.Id
                            });
                        }
                    }
                }
                else
                {
                    result = this.OwnershipService.UpdateOwnership(nowner);
                }
                if (result)
                {
                    return View("CreateSuccess");
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


        public ActionResult UpdateOwnershipById(int id)
        {
            var oOwner = this.OwnershipService.GetOwnership(id);
            var owner = Mapper.Map<OwnershipViewModel>(oOwner);            
            return View("CreateOwnership",owner);
        }

        public ActionResult UpdateOwnership(OwnershipViewModel owner)
        {            
            var nOwner = new Ownership();
            
            nOwner = Mapper.Map<Ownership>(owner);            
            this.OwnershipService.UpdateOwnership(nOwner);
            return View();
        }

        public ActionResult DeleteOwnership(int id)
        {                    
            this.OwnershipService.DeleteOwnership(id);
            return View("DeleteSuccess");
        }
        
        public ActionResult List()
        {         
         
            try
            {
                var owners = this.OwnershipService.GetAll();
                return View(owners);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


    }
}