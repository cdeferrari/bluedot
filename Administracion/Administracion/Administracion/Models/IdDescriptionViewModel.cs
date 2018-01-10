using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class IdDescriptionViewModel
    {
        public virtual int Id { get; set;   }
        public virtual string Description { get; set; }        
    }

}
