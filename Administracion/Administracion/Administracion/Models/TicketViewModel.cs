using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class TicketViewModel
    {
        public virtual int Id { get; set; }
        public virtual string Customer { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual int SpendItemId { get; set; }
        public virtual Status Status { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
        public virtual DateTime LimitDate { get; set; }
        public virtual int FunctionalUnitId { get; set; }
        public virtual FunctionalUnit FunctionalUnit { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual IEnumerable<SelectListItem> StatusList { get; set; }
        public virtual IEnumerable<SelectListItem> PriorityList { get; set; }
        public virtual IEnumerable<SelectListItem> WorkersList { get; set; }
        public virtual IEnumerable<SelectListItem> ManagerList { get; set; }
        public virtual IEnumerable<SelectListItem> ProviderList { get; set; }
        public virtual IEnumerable<SelectListItem> UsersList { get; set; }
        public virtual IEnumerable<SelectListItem> ConsortiumList { get; set; }
        public virtual IEnumerable<SelectListItem> FunctionalUnitList { get; set; }
        public virtual IEnumerable<SelectListItem> SpendItemList { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual User Creator { get; set; }
        public virtual Consortium Consortium { get; set; }
        public virtual IList<MessageViewModel> Messages { get; set; }
        public virtual IList<Task> Tasks { get; set; }

        public virtual TaskViewModel Task { get; set; }
    }
}