using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Implementations.Consortiums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    public class ConsortiumController : Controller
    {
        public IConsortiumService TicketService { get; set; }
        // GET: Backlog
        public ActionResult CreateConsortium()
        {
            return View();
        }

        public ActionResult CreateConsortium(ConsortiumViewModel consortium)
        {
         
            var nConsortium = new Consortium();
            
            var nConsortium = Mapper.Map<Consortium>(consortium);
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
            
            var nConsortium = Mapper.Map<Consortium>(consortium);            
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