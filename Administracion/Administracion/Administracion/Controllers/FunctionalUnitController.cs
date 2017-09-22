using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.FunctionalUnits;
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
    public class FunctionalUnitController : Controller
    {
        public virtual IFunctionalUnitService FunctionalUnitService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateFunctionalUnit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFunctionalUnit(FunctionalUnitViewModel unit)
        {
         
            var nunit = new FunctionalUnit(); 
            
            nunit = Mapper.Map<FunctionalUnit>(unit);
            try
            {
                this.FunctionalUnitService.CreateFunctionalUnit(nunit);
                return View("CreateSuccess");
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


        public ActionResult UpdateFunctionalUnitById(int id)
        {
            var oUnit = this.FunctionalUnitService.GetFunctionalUnit(id);
            var unit = Mapper.Map<FunctionalUnitViewModel>(oUnit);            
            return View("CreateFunctionalUnit",unit);
        }

        public ActionResult UpdateFunctionalUnit(FunctionalUnitViewModel unit)
        {            
            var nunit = new FunctionalUnit();
            
            nunit = Mapper.Map<FunctionalUnit>(unit);            
            this.FunctionalUnitService.UpdateFunctionalUnit(nunit);
            return View();
        }

        public ActionResult DeleteFunctionalUnit(int id)
        {                    
            this.FunctionalUnitService.DeleteFunctionalUnit(id);
            return View();
        }
        
        public ActionResult List()
        {         
         
            try
            {
                var units = this.FunctionalUnitService.GetAll();
                return View(units);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


    }
}