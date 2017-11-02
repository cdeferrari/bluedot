using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Owners;
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
    [CustomAuthorize(Roles.Root)]
    public class FunctionalUnitController : Controller
    {
        public virtual IFunctionalUnitService FunctionalUnitService { get; set; }
        public virtual IOwnershipService OwnershipService { get; set; }
        public virtual IOwnerService OwnersService { get; set; }

        public ActionResult Index()
        {
            try
            {
                var units = this.FunctionalUnitService.GetAll();
                var unitsView = Mapper.Map<List<FunctionalUnitViewModel>>(units);
                return View("List",unitsView);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateFunctionalUnit()
        {
            var ownerships = this.OwnershipService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Address.Street + " " + x.Address.Number.ToString()
            });

            var owners = this.OwnersService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });



            var viewModel = new FunctionalUnitViewModel()
            {
                Ownerships = new SelectList(ownerships, "Value", "Text"),
                Owners = new SelectList(owners, "Value", "Text")
            };


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateFunctionalUnit(FunctionalUnitViewModel unit)
        {
         
            var nunit = Mapper.Map<FunctionalUnit>(unit);           

            try
            {
                var result = false;
                if (nunit.Id == 0)
                {
                    result = this.FunctionalUnitService.CreateFunctionalUnit(nunit);
                }
                else
                {
                    result = this.FunctionalUnitService.UpdateFunctionalUnit(nunit);
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


        public ActionResult UpdateFunctionalUnitById(int id)
        {
            var oUnit = this.FunctionalUnitService.GetFunctionalUnit(id);
            var ownerships = this.OwnershipService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Address.Street + " " + x.Address.Number.ToString()
            });


            var owners = this.OwnersService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name+ " " + x.User.Surname
            });


            var unit = Mapper.Map<FunctionalUnitViewModel>(oUnit);
            unit.Ownerships = ownerships;
            unit.Owners = owners;
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
            return View("DeleteSuccess");
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