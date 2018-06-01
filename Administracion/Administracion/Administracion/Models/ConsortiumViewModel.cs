using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ConsortiumViewModel
    {
        public virtual int Id { get; set; }

        [DisplayName("Nombre")]
        public virtual string FriendlyName { get; set; }
        public virtual string CUIT { get; set; }
        [DisplayName("Lista")]
        public virtual string MailingList { get; set; }
        [DisplayName("Teléfono")]
        public virtual string Telephone { get; set; }
        public virtual int AdministrationId {get; set;}
        public virtual int OwnershipId {get; set;}        
        public virtual IEnumerable<SelectListItem> Administrations { get; set; }
        public virtual IEnumerable<SelectListItem> Ownerships { get; set; }
        public virtual IEnumerable<SelectListItem> CommonDataItems { get; set; }
        public virtual IEnumerable<SelectListItem> Provinces { get; set; }
        public virtual IEnumerable<SelectListItem> Cities { get; set; }
        public virtual OwnershipViewModel Ownership { get; set; }
        public virtual HttpPostedFileBase Image { get; set; }
        public virtual string ClaveSuterh { get; set; }
    }
}