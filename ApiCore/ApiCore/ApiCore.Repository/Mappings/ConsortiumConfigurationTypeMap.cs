using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class ConsortiumConfigurationTypeMap : EntityMap<ConsortiumConfigurationType>
    {
        public ConsortiumConfigurationTypeMap() : base("tipo_configuracion_consorcio")
        {            
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            this.Property(x => x.IsPercentage).IsRequired().HasColumnName("isPercentage");
        }

    }
}

