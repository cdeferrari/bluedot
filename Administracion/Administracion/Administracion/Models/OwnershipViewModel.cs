using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class OwnershipViewModel
    {
        public virtual int Id { get; set; }
        public virtual AddressViewModel Address { get; set; }
        public virtual string Category { get; set; }
        public virtual HttpPostedFileBase Image { get; set; }
        public virtual List<FunctionalUnitViewModel> FunctionalUnits { get; set; }
        public virtual List<CommonData> CommonData { get; set; }
    }
}