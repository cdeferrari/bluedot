using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class TaskViewModel
    {
        public virtual int Id { get; set; }
        public virtual int TicketId { get; set; }
        public virtual int PriorityId { get; set; }        
        public virtual string Description { get; set; }
        public virtual Ticket Ticket { get; set; }      
        public virtual Worker Worker { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual IList<TaskHistoryViewModel> TaskHistory { get; set; }
        public virtual TaskHistoryViewModel TaskFollow { get; set; }
    }
}