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
    public class MessageViewModel
    {
        public virtual int TicketId { get; set; }
        public virtual UserViewModel Sender { get; set; }
        public virtual UserViewModel Receiver { get; set; }
        [DisplayName("Contenido")]
        public virtual string Content { get; set; }
        public virtual DateTime Date { get; set; }
    }
}