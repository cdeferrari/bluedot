using Administracion.DomainModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class UnitConfigurationViewModel
    {        
        public virtual List<UnitConfigurationType> ConfigurationTypes { get; set; }
        public virtual Dictionary<int, UnitConfiguration> Configurations { get; set; }
        public virtual List<UnitConfigurationItemViewModel> UnitConfigurations { get; set; }
        public virtual int UnitId { get; set; }
    }
}