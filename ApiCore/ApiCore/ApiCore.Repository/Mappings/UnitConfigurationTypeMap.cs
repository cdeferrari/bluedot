using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class UnitConfigurationTypeMap : EntityMap<UnitConfigurationType>
    {
        public UnitConfigurationTypeMap() : base("tipo_configuracion_unidad")
        {            
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            this.Property(x => x.IsPercentage).IsRequired().HasColumnName("isPercentage");
        }

    }
}

