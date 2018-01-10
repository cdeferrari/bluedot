using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class ProvinceMap : EntityMap<Province>
    {
        public ProvinceMap() : base("provincia")
        {                        
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            this.HasRequired(x => x.Country).WithMany().Map(x => x.MapKey("country_id"));
            this.HasMany(x => x.Cities).WithRequired(x => x.Province).Map(x => x.MapKey("province_id"));
        }

    }
}

