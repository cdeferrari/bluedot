using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class ConsortiumViewModel
    {
        public virtual string FriendlyName { get; set; }
        public virtual string CUIT { get; set; }
        public virtual string MailingList { get; set; }
        public virtual AdministrationViewModel Administration {get; set;}
        public virtual OwnershipViewModel OwnershipId {get; set;}
        public virtual AddressViewModel Address{ get; set; }

        public virtual List<AdministrationViewModel> Administrations { get; set; }
        public virtual List<OwnershipViewModel> Ownerships { get; set; }
    }
}