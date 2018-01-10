using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Implementations.Tickets;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{

    public class DataBaseController : Controller
    {

        [CustomAuthorize(Roles.All)]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles.All)]
        public ActionResult User()
        {
            return View();
        }

    }
}