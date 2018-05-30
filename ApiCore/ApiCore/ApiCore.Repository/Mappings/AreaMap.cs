using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class AreaMap : EntityMap<Area>
    {
        public AreaMap() : base("area")
        {            
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

