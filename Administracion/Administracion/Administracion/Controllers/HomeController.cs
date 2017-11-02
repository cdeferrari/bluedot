using Administracion.DomainModel.Enum;
using Administracion.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Roles.Root)]
        public ActionResult Index()
        {
            return Redirect("DataBase/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}