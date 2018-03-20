using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class CreateSpendViewModel
    {        
        public virtual IEnumerable<SelectListItem> SpendTypes { get; set; }
        public virtual IEnumerable<SelectListItem> Providers { get; set; }
        public virtual IEnumerable<SelectListItem> Managers { get; set; }
        public virtual IEnumerable<SelectListItem> Workers { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual int ManagerId { get; set; }
        public virtual int WorkerId { get; set; }
        public virtual int ProviderId { get; set; }
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual int SpendTypeId { get; set; }
        public virtual int SpendItemId { get; set; }
        public virtual bool Required { get; set; }
        public virtual bool ForAll { get; set; }

    }
}