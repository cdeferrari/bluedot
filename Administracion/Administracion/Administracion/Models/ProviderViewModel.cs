using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ProviderViewModel
    {
        

        public virtual int Id { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual AddressViewModel Address { get; set; }
        [DisplayName("Rubro")]
        public virtual string Item { get; set; }
        public virtual IEnumerable<SelectListItem> Administrations { get; set; }        
        public virtual int AdministrationId { get; set; }

    }
}