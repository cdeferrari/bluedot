using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Owners;
using AutoMapper;
using System;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    [Authorize]
    public class OwnerController : Controller
    {
        public virtual IOwnerService OwnerService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateOwner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOwner(OwnerViewModel owner)
        {
         
            var nowner = new Owner(); 
            
            nowner = Mapper.Map<Owner>(owner);
            try
            {
                this.OwnerService.CreateOwner(nowner);
                return View("CreateSuccess");
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


        public ActionResult UpdateOwnerById(int id)
        {
            var oOwner = this.OwnerService.GetOwner(id);
            var owner = Mapper.Map<OwnerViewModel>(oOwner);            
            return View("CreateOwner",owner);
        }

        public ActionResult UpdateOwner(OwnerViewModel owner)
        {            
            var nOwner = new Owner();
            
            nOwner = Mapper.Map<Owner>(owner);            
            this.OwnerService.UpdateOwner(nOwner);
            return View();
        }

        public ActionResult DeleteOwner(int id)
        {                    
            this.OwnerService.DeleteOwner(id);
            return View();
        }
        
        public ActionResult List()
        {         
         
            try
            {
                var owners = this.OwnerService.GetAll();
                return View(owners);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


    }
}