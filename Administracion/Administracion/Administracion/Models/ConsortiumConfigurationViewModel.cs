using Administracion.DomainModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ConsortiumConfigurationViewModel
    {        
        public virtual IList<ConsortiumConfiguration> Configurations { get; set; }
        public virtual IList<ConsortiumConfigurationType> ConfigurationType { get; set; }        
        public virtual int ConsortiumId { get; set; }
    }
}