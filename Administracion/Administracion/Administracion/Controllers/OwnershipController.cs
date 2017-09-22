using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Implementations.Tickets;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    [Authorize]
    public class OwnershipController : Controller
    {
        public virtual IOwnershipService OwnershipService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateOwnership()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOwnership(OwnershipViewModel owner)
        {
         
            var nowner = new Ownership(); 
            
            nowner = Mapper.Map<Ownership>(owner);
            try
            {
                this.OwnershipService.CreateOwnership(nowner);
                return View("CreateSuccess");
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
            return View();
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