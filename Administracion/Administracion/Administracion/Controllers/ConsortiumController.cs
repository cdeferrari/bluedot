using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Implementations.Consortiums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    //[Authorize]
    public class ConsortiumController : Controller
    {
        public IConsortiumService ConsortiumService { get; set; }
        public IAdministrationService AdministrationService { get; set; }
        public IOwnershipService OwnershipService { get; set; }
        // GET: Backlog
        public ActionResult Index()
        {
            
            return View();
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


        [HttpPost]
        public ActionResult CreateConsortium(ConsortiumViewModel consortium)
        {
         
            var nConsortium = new Consortium();
            
            nConsortium = Mapper.Map<Consortium>(consortium);
            this.ConsortiumService.CreateConsortium(nConsortium);            
            return View();
        }


        public ActionResult UpdateConsortium(int id)
        {
            var oConsortium = this.ConsortiumService.GetConsortium(id);
            var consortium = Mapper.Map<ConsortiumViewModel>(oConsortium);            
            return View(consortium);
        }

        public ActionResult UpdateConsortium(ConsortiumViewModel consortium)
        {            
            var nConsortium = new Consortium();
            
            nConsortium = Mapper.Map<Consortium>(consortium);            
            this.ConsortiumService.UpdateConsortium(nConsortium);
            return View();
        }

        public ActionResult DeleteConsortium(int id)
        {                    
            this.ConsortiumService.DeleteConsortium(id);
            return View();
        }
    }
}