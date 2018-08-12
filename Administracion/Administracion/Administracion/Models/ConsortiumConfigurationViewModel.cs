using Administracion.DomainModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ConsortiumConfigurationViewModel
    {        
        public virtual List<ConsortiumConfigurationType> ConfigurationTypes { get; set; }
        public virtual Dictionary<int, ConsortiumConfiguration> Configurations { get; set; }
        public virtual List<ConsortiumConfigurationItemViewModel> ConsortiumConfigurations { get; set; }
        public virtual int ConsortiumId { get; set; }
    }
}