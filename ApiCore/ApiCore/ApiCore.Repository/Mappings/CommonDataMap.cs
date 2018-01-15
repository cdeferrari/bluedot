using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class CommonDataMap : EntityMap<CommonData>
    {
        public CommonDataMap() : base("dato_comun")
        {
            this.HasRequired(x => x.Item).WithMany().Map(x => x.MapKey("common_data_item_id"));
            this.Property(x => x.Have).IsRequired().HasColumnName("have");
        }

    }
}

