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
    public class ConsortiumSecureViewModel
    {
        public virtual int Id { get; set; }
        public virtual IEnumerable<SelectListItem> SecureStatus { get; set; }
        public virtual int SecureStatusId { get; set; }
        [DisplayName("Número de poliza")]
        public virtual string PolizyNumber { get; set; }
        [DisplayName("Marca")]
        public virtual string Brand { get; set; }
        [DisplayName("Vencimiento")]
       
        public virtual DateTime LimitDate { get; set; }        
        public virtual int ConsortiumId { get; set; }        
    }
}