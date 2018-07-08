using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class OwnerMap : EntityMap<Owner>
    {
        public OwnerMap() : base("propietario")
        {
            this.HasRequired(x => x.User).WithMany().Map(x => x.MapKey("user_id"));
            this.Property(x => x.PaymentTypeId).IsOptional().HasColumnName("payment_type_id");
            this.HasMany(x => x.FunctionalUnits).WithOptional(x => x.Owner).Map(x => x.MapKey("owner_id"))
            .WillCascadeOnDelete(false);
        }

    }
}

