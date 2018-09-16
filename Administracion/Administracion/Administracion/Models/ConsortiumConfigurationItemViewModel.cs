using Administracion.DomainModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ConsortiumConfigurationItemViewModel
    {
        public virtual int ConsortiumConfigurationTypeId { get; set; }        
        public virtual decimal Value { get; set; }
    }
}