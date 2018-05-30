using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ElevatorControlViewModel
    {
        public virtual int Id { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual int ProviderId { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime ControlDate { get; set; }
    }

}
