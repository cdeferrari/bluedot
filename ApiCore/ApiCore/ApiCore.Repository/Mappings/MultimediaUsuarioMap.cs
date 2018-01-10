
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class MultimediaUsuarioMap : EntityMap<MultimediaUsuario>
    {
        public MultimediaUsuarioMap() : base("multimedia_usuario")
        {            
            this.Property(x => x.Url).IsRequired().HasColumnName("url");            
        }

    }
}

