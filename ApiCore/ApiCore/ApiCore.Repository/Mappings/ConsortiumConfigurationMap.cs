using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ConsortiumConfigurationMap : EntityMap<ConsortiumConfiguration>
    {
        public ConsortiumConfigurationMap() : base("configuracion_consorcio")
        {
            this.Property(x => x.Value).IsRequired().HasColumnName("value");            
            this.Property(x => x.ConfigurationDate).IsRequired().HasColumnName("configuration_date");
            this.HasRequired(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));
            this.HasRequired(x => x.Type).WithMany().Map(x => x.MapKey("configuration_type_id"));
            
        }

    }
}

