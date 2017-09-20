using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class AdministrationViewModel
    {
        public virtual string Name { get; set; }
        public virtual string CUIT { get; set; }
        public virtual AddressViewModel Address { get; set; }        
    }
}