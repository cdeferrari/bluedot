using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class TicketListViewModel
    {
        public virtual List<TicketViewModel> Tickets { get; set; }
        public virtual int Closed { get; set; }
        public virtual int Open { get; set; }
        public virtual int All { get; set; }
        public virtual int Blockers { get; set; }
        public virtual int SelectedIndex { get; set; }
        
    }
}