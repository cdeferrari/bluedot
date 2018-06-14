﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Task
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual BacklogUser Creator { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime? CloseDate { get; set; }        
        public IList<Spend> Spends { get; set; }
        public virtual IList<TaskHistory> TaskHistory { get; set; }
    }
}