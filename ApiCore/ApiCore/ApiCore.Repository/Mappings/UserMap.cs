using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class UserMap : EntityMap<User>
    {
        public UserMap() : base("usuario")
        {
            
            this.Property(x => x.DNI).IsRequired().HasColumnName("dni");
            this.Property(x => x.CUIT).IsRequired().HasColumnName("cuit");
            this.Property(x => x.Name).IsRequired().HasColumnName("name");
            this.Property(x => x.Surname).IsOptional().HasColumnName("surname");                                
            this.HasOptional(x => x.ContactData).WithMany().Map(x => x.MapKey("data_contact_id"));
            
        }

    }
}

