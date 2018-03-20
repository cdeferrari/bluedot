using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class SpendViewModel
    {
        public virtual IList<Spend> Spends { get; set; }
        public virtual IEnumerable<SelectListItem> SpendTypes { get; set; }
        public virtual IEnumerable<SelectListItem> SpendItems { get; set; }                
        public virtual int ConsortiumId { get; set; }
    }
}