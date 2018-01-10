using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class SystemModel
    {
        public virtual IList<IdDescriptionViewModel> LaboralUnions { get; set; }
        public virtual IList<IdDescriptionViewModel> PaymentTypes { get; set; }

        public virtual IList<IdDescriptionViewModel> Provinces { get; set; }

        public virtual IList<IdDescriptionViewModel> Cities { get; set; }
    }
}