
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class OwnershipMap : EntityMap<Ownership>
    {
        public OwnershipMap() : base("Propiedad")
        {            
            this.HasRequired(x => x.Address).WithMany().Map(x => x.MapKey("direction_id"));
            this.Property(x => x.Category).IsOptional().HasColumnName("category");
            this.HasMany(x => x.FunctionalUnits).WithRequired(x => x.Ownership).Map(x => x.MapKey("property_id"));
            this.HasMany(x => x.CommonData).WithRequired(x => x.Ownership).Map(x => x.MapKey("ownership_id"));
            this.HasMany(x => x.Multimedia).WithRequired(x => x.Ownership).Map(x => x.MapKey("ownership_id"));
        }

    }
}

