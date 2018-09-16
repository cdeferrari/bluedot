using Administracion.DomainModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class UnitConfigurationItemViewModel
    {
        public virtual int UnitConfigurationTypeId { get; set; }        
        public virtual decimal Value { get; set; }
    }
}