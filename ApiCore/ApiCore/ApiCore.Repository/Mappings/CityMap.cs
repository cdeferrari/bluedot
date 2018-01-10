using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class CityMap : EntityMap<City>
    {
        public CityMap() : base("ciudad")
        {                        
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            this.HasRequired(x => x.Province).WithMany().Map(x => x.MapKey("province_id"));
        }

    }
}

