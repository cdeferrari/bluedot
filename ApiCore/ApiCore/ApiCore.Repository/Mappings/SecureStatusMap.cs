using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class SecureStatusMap : EntityMap<SecureStatus>
    {
        public SecureStatusMap() : base("estado_seguro")
        {            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

