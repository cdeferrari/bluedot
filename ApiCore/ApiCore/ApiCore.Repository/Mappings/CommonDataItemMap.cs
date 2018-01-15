using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class CommonDataItemMap : EntityMap<CommonDataItem>
    {
        public CommonDataItemMap() : base("dato_comun_item")
        {
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            
        }

    }
}

