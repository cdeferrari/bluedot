using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ChoseConsortiumViewModel
    {        
        public virtual IEnumerable<SelectListItem> Consortiums { get; set; }        
        public virtual int ConsortiumId { get; set; }
    }
}