using Administracion.DomainModel.Enum;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Administracion.Models
{
    public class AddressViewModel
    {
        [DisplayName("Calle")]
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
    }

}
