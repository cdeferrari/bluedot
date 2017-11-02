using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class LaboralUnionMap : EntityMap<LaboralUnion>
    {
        public LaboralUnionMap() : base("sindicato")
        {
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            
        }

    }
}

