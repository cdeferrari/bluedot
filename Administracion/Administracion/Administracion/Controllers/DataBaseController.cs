using Administracion.DomainModel;
using Administracion.Models;
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
        
        public ActionResult Index()
        {
            return View();
        }

    }
}