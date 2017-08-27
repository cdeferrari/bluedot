using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class PriorityMap : EntityMap<Priority>
    {
        public PriorityMap() : base("prioridad")
        {            
            this.Property(x => x.Value).IsRequired().HasColumnName("value");
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

