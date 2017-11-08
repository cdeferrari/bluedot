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
        [DisplayName("Codio Postal")]
        public string PostalCode { get; set; }
        [DisplayName("Numero")]
        public string Number { get; set; }
        [DisplayName("Ciudad")]
        public string City { get; set; }
        [DisplayName("Provincia")]
        public string Province { get; set; }
    }

}
