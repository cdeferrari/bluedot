using ApiCore.DomainModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ApiCore.Repository.Mappings
{
    public abstract class EntityMap<T> : EntityTypeConfiguration<T>  where T : Entity
    {
        public EntityMap(string nombreTabla)
        {
            this.ToTable(nombreTabla);
            this.HasKey(x => x.Id);
            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}