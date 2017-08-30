using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class FunctionalUnitMap : EntityMap<FunctionalUnit>
    {
        public FunctionalUnitMap() : base("unidad")
        {            
            this.Property(x => x.PropertyId).IsRequired().HasColumnName("property_id");            
            this.Property(x => x.Floor).IsRequired().HasColumnName("piso");
            this.Property(x => x.Dto).IsOptional().HasColumnName("dto");
            this.Property(x => x.OwnerId).IsRequired().HasColumnName("owner_id");
            this.Property(x => x.RenterId).IsRequired().HasColumnName("renter_id");            
        }

    }
}

