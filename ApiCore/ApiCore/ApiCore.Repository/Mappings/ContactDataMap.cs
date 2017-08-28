using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ContactDataMap : EntityMap<ContactData>
    {
        public ContactDataMap() : base("datos_contacto")
        {
            
            this.Property(x => x.Telephone).IsRequired().HasColumnName("telephone");
            this.Property(x => x.Cellphone).IsRequired().HasColumnName("cellphone");
            this.Property(x => x.Email).IsRequired().HasColumnName("email");            
        }

    }
}

