using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto;
using Administracion.Dto.City;
using Administracion.Dto.CommonData;
using Administracion.Dto.Consortium;
using Administracion.Dto.List;
using Administracion.Dto.Province;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.CommonData;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.LaboralUnion;
using Administracion.Services.Contracts.Lists;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.Owners;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.PaymentTypesService;
using Administracion.Services.Contracts.Provinces;
using Administracion.Services.Contracts.Renters;
using Administracion.Services.Contracts.Status;
using Administracion.Services.Contracts.TaskResult;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Implementations.Consortiums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    
    [CustomAuthorize(Roles.All)]
    public class SystemController : Controller
    {
        public virtual ICityService CityService { get; set; }
        public virtual IProvinceService ProvinceService { get; set; }
        public virtual IPaymentTypesService PaymentTypeService { get; set; }
        public virtual ILaboralUnionService LaboralUnionService { get; set; }
        // GET: Backlog
        public ActionResult Index()
        {
            try
            {
                var paymentTypes = this.PaymentTypeService.GetAll();
                var paymentTypesViewModel = Mapper.Map<List<IdDescriptionViewModel>>(paymentTypes);

                var laboralUnions = this.LaboralUnionService.GetAll();
                var laboralUnionsViewModel = Mapper.Map<List<IdDescriptionViewModel>>(laboralUnions);

                var cities = this.CityService.GetAll();
                var citiesViewModel = Mapper.Map<List<IdDescriptionViewModel>>(cities);

                var provinces = this.ProvinceService.GetAllProvinces();
                var provincesViewModel = Mapper.Map<List<IdDescriptionViewModel>>(provinces);

                var SystemModel = new SystemModel()
                {
                    Cities = citiesViewModel,
                    Provinces = provincesViewModel,
                    LaboralUnions = laboralUnionsViewModel,
                    PaymentTypes = paymentTypesViewModel

                };

                return View("Index", SystemModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

        [HttpGet]
        public ActionResult CreateLaboralUnion()
        {
            var viewModel = new IdDescriptionViewModel();            
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateCity()
        {
            var viewModel = new CityViewModel();
            var provinces = this.ProvinceService.GetAllProvinces().Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.Id.ToString()
            });
            
            viewModel.ProvincesList = provinces;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateProvince()
        {
            var viewModel = new ProvinceViewModel();
            var countryList = new List<SelectListItem>() { new SelectListItem()
            {
                Text = "Argentina",
                Value = ConfigurationManager.AppSettings["default_country_id"]
            }};

            viewModel.CountriesList = countryList;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreatePaymentType()
        {
            var viewModel = new IdDescriptionViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateProvince(ProvinceViewModel province)
        {

            var nprov = new ProvinceRequest()
            {
                Description = province.Description,
                CountryId = province.CountryId
            };

            try
            {
                var result = false;
                if (province.Id == 0)
                {
                    result = this.ProvinceService.CreateProvince(nprov);
                }

                if (result)
                {
                    return Redirect("/System/Index");
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

        [HttpPost]
        public ActionResult CreateCity(CityViewModel city)
        {

            var ncity = new CityRequest()
            {
                Description = city.Description,
                ProvinceId = city.ProvinceId
            };

            try
            {
                var result = false;
                if (city.Id == 0)
                {
                    result = this.CityService.CreateCity(ncity);
                }

                if (result)
                {
                    return Redirect("/System/Index");
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


        [HttpPost]
        public ActionResult CreatePaymentType(IdDescriptionViewModel paymentType)
        {
            
            var npt = new DescriptionRequest()
            {
                Description = paymentType.Description
            };
            
            try
            {
                var result = false;
                if (paymentType.Id == 0)
                {
                    result = this.PaymentTypeService.CreatePaymentType(npt);
                }
                
                if (result)
                {
                    return Redirect("/System/Index");
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

        [HttpPost]
        public ActionResult CreateLaboralUnion(IdDescriptionViewModel laboralUnion)
        {

            var nlu = new DescriptionRequest()
            {
                Description = laboralUnion.Description
            };

            try
            {
                var result = false;
                if (laboralUnion.Id == 0)
                {
                    result = this.LaboralUnionService.CreateLaboralUnion(nlu);
                }

                if (result)
                {
                    return Redirect("/System/Index");
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

        public ActionResult DeleteLaboralUnion(int id)
        {
            this.LaboralUnionService.DeleteLaboralUnion(id);
            return Redirect("/System/Index");
        }

        public ActionResult DeletePaymentType(int id)
        {
            this.PaymentTypeService.DeletePaymentType(id);
            return Redirect("/System/Index");
        }

        public ActionResult DeleteCity(int id)
        {
            this.CityService.DeleteCity(id);
            return Redirect("/System/Index");
        }

        public ActionResult DeleteProvince(int id)
        {
            this.ProvinceService.DeleteProvince(id);
            return Redirect("/System/Index");
        }
    }
}