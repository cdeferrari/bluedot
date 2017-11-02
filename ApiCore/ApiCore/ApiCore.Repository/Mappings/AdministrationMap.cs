
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class AdministrationMap : EntityMap<Administration>
    {
        public AdministrationMap() : base("administracion")
        {
            
            this.Property(x => x.Name).IsRequired().HasColumnName("name");
            this.Property(x => x.CUIT).IsRequired().HasColumnName("cuit");
            
            this.Property(x => x.StartDate).IsRequired().HasColumnName("start_date");
            this.HasMany(x => x.Workers).WithRequired(x => x.Administration).Map(x => x.MapKey("administration_id"));

            this.HasRequired(x => x.Address).WithMany().Map(x => x.MapKey("direction_id"));
                        

        }

    }
}

