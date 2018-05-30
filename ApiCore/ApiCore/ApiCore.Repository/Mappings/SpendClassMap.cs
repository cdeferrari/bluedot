using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class SpendClassMap : EntityMap<SpendClass>
    {
        public SpendClassMap() : base("clase_gasto")
        {                        
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

