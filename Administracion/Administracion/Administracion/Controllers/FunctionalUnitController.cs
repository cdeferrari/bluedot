using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.Owner;
using Administracion.Dto.Renter;
using Administracion.Dto.Unit;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Owners;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.Renters;
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
        public virtual IRenterService RenterService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }

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
        public ActionResult CreateFunctionalUnit(int id)
        {
            var consortium = this.ConsortiumService.GetConsortium(id);

            var owners = this.OwnersService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });


            var renters = this.RenterService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var viewModel = new FunctionalUnitViewModel()
            {
                ConsortiumId = consortium.Id,
               Renters = new SelectList(renters, "Value", "Text"),
                Owners = new SelectList(owners, "Value", "Text")
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateFunctionalUnit(FunctionalUnitViewModel unit)
        {

            var consortium = this.ConsortiumService.GetConsortium(unit.ConsortiumId);            
            var nunit = Mapper.Map<FunctionalUnit>(unit);
            nunit.Ownership = consortium.Ownership;
            Renter Renter = null;
            if (unit.RenterId != 0)
            {
                Renter = this.RenterService.GetRenter(unit.RenterId);
            }

            Owner Owner = null;
            if(unit.OwnerId!= 0)
            {
                Owner = this.OwnersService.GetOwner(unit.OwnerId);
            }
            

            try
            {
                var result = false;
                var entity = Mapper.Map<FunctionalUnitRequest>(nunit);
                if (nunit.Id == 0)
                {
                    
                    var entidad = this.FunctionalUnitService.CreateFunctionalUnit(entity);
                    if (entidad.Id >0)
                    {

                        if (Renter != null)
                        {
                            var renterRequest = new RenterRequest()
                            {
                                Id = Renter.Id,
                                FunctionalUnitId = entidad.Id,
                                UserId = Renter.User.Id,
                                PaymentTypeId = 1
                            };

                            this.RenterService.UpdateRenter(renterRequest);
                        }

                        if (Owner != null)
                        {
                            var ownerRequest = new OwnerRequest()
                            {
                                Id = Owner.Id,
                                FunctionalUnitId = entidad.Id,
                                UserId = Owner.User.Id
                            };
                            this.OwnersService.UpdateOwner(ownerRequest);
                        }
                        
                        result = true;
                    }
                }
                else
                {
                    result = this.FunctionalUnitService.UpdateFunctionalUnit(entity);
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
            var entity = Mapper.Map<FunctionalUnitRequest>(nunit);
            this.FunctionalUnitService.UpdateFunctionalUnit(entity);
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
