using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ProviderMap : EntityMap<Provider>
    {
        public ProviderMap() : base("proveedor")
        {
            this.HasRequired(x => x.User).WithMany().Map(x => x.MapKey("user_id"));
            this.Property(x => x.Item).IsRequired().HasColumnName("item");
            this.HasOptional(x => x.Address).WithMany().Map(x => x.MapKey("address_id"));
        }

    }
}

