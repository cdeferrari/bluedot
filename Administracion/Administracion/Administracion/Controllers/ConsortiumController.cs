using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.Consortiums;
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
        // GET: Backlog
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult CreateConsortium()
        {
            var viewModel = new ConsortiumViewModel()
            {
                Administrations = Mapper.Map<List<AdministrationViewModel>>(this.AdministrationService.GetAll()),
                //Ownerships =
            };
            
            return View();
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