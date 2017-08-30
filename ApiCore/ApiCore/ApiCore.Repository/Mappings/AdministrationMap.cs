
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class AdministrationMap : EntityMap<Administration>
    {
        public AdministrationMap() : base("administracion")
        {
            
            this.Property(x => x.Name).IsRequired().HasColumnName("name");
            this.Property(x => x.CUIT).IsRequired().HasColumnName("cuit");
            this.Property(x => x.DirectionId).IsRequired().HasColumnName("direction_id");
            this.Property(x => x.StartDate).IsRequired().HasColumnName("start_date");
            

        }

    }
}

