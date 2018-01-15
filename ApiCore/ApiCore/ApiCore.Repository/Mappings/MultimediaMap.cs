
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class MultimediaMap : EntityMap<Multimedia>
    {
        public MultimediaMap() : base("multimedia")
        {            
            this.Property(x => x.Url).IsRequired().HasColumnName("url");
            this.Property(x => x.MultimediaTypeId).IsRequired().HasColumnName("multimedia_type_id");          
        }

    }
}

