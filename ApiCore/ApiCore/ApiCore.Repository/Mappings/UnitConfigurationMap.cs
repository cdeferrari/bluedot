using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class UnitConfigurationMap : EntityMap<UnitConfiguration>
    {
        public UnitConfigurationMap() : base("configuracion_unidad")
        {
            this.Property(x => x.Value).IsRequired().HasColumnName("value");            
            this.Property(x => x.ConfigurationDate).IsRequired().HasColumnName("configuration_date");
            this.HasRequired(x => x.Unit).WithMany().Map(x => x.MapKey("unit_id"));
            this.HasRequired(x => x.Type).WithMany().Map(x => x.MapKey("configuration_type_id"));
            
        }

    }
}

