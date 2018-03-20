using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class PatrimonyStatusViewModel
    {
        public virtual IList<PatrimonyStatus> Status { get; set; }
        public virtual int ConsortiumId { get; set; }
    }
}