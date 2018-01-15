using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class CountryMap : EntityMap<Country>
    {
        public CountryMap() : base("pais")
        {                        
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

