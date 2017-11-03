using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class RenterMap : EntityMap<Renter>
    {
        public RenterMap() : base("inquilino")
        {
            this.HasRequired(x => x.User).WithMany().Map(x => x.MapKey("user_id"));
            //this.HasRequired(x => x.FunctionalUnit).WithOptional(x => x.Renter).Map(x => x.MapKey("functional_unit_id"));
            //this.HasRequired(x => x.FunctionalUnit).WithRequiredDependent(x => x.Renter).Map(x => x.MapKey("functional_unit_id"));
            this.Property(x => x.PaymentTypeId).IsRequired().HasColumnName("payment_type_id");
            this.Property(x => x.FunctionalUnitId).IsOptional().HasColumnName("functional_unit_id");
        }

    }
}

