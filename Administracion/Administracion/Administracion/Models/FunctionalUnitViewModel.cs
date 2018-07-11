using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class FunctionalUnitViewModel
    {
        public virtual int Id { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string OwnershipAddress { get; set; }
        [DisplayName("Piso")]
        public virtual int Floor { get; set; }
        [DisplayName("Departamento")]
        public virtual string Dto { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual int RenterId { get; set; }

        [DisplayName("Numero")]
        public virtual int Number { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Renter Renter { get; set; }

        public virtual IEnumerable<SelectListItem> Ownerships { get; set; }
        public virtual IEnumerable<SelectListItem> Owners { get; set; }
        public virtual IEnumerable<SelectListItem> Renters { get; set; }
    }
}