using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class FunctionalUnitViewModel
    {
        public virtual int Id { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual int Floor { get; set; }
        public virtual int Dto { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual int RenterId { get; set; }
        public virtual IEnumerable<SelectListItem> Ownerships { get; set; }
        public virtual IEnumerable<SelectListItem> Owners { get; set; }
    }
}