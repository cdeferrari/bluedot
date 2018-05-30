using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class TicketHistoryViewModel
    {
        public virtual int Id { get; set;   }
        public virtual string Coment { get; set; }
        public virtual DateTime FollowDate { get; set; }

        public virtual int TicketId { get; set; }
    }

}
