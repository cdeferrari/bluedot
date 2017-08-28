using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class RoleMap : EntityMap<Role>
    {
        public RoleMap() : base("rol")
        {            
         
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

