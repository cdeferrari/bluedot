using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ConsortiumDetailsViewModel
    {
        public virtual int Id { get; set; }
        public virtual string FriendlyName { get; set; }
        public virtual string CUIT { get; set; }
        public virtual string MailingList { get; set; }
        public virtual string Telephone { get; set; }
        public virtual int AdministrationId {get; set;}              
        public virtual OwnershipViewModel Ownership { get; set; }     
        public virtual AdministrationViewModel Administration { get; set; }
        public virtual IList<List> Checklists { get; set; }
        public virtual IList<Manager> Managers { get; set; }
        public virtual IList<ConsortiumSecure> ConsortiumSecure { get; set; }
        public virtual int TicketQuantity { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual IList<FireExtinguisherControl> FireExtinguisherControls { get; set; }
        public virtual IList<ElevatorControl> ElevatorControls { get; set; }
        public virtual IEnumerable<SelectListItem> Providers { get; set; }
        public virtual int ProviderId { get; set; }
    }
}