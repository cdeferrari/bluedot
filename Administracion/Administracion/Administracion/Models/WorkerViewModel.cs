using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class WorkerViewModel : UserViewModel
    {
        public virtual IEnumerable<SelectListItem> Consortiums { get; set; }
    }
}