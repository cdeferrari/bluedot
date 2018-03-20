using System;
using System.Web.Mvc;
using Administracion.Services.Contracts.PatrimonyStatuss;
using Administracion.Services.Contracts.Consortiums;
using AutoMapper;
using System.Collections.Generic;
using Administracion.Models;

namespace Administracion.Controllers
{
    public class PatrimonyController : Controller
    {
        public virtual IPatrimonyStatusService PatrimonyStatusService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }

        public ActionResult Index(int id)
        {
            try
            {
                var patrimonyStatusList = this.PatrimonyStatusService.GetByConsortiumId(id);

                var model = new PatrimonyStatusViewModel()
                {
                    ConsortiumId = id,
                    Status = patrimonyStatusList
                };
                return View("List", model);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }

        public ActionResult CloseMonth(int consortiumId)
        {
            this.ConsortiumService.CloseMonth(consortiumId);
            return Redirect(string.Format("/Patrimony/Index?Id={0}", consortiumId));
        }


    }
}