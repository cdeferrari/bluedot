using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class ManagerPositionMap : EntityMap<ManagerPosition>
    {
        public ManagerPositionMap() : base("puesto_encargado")
        {            
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

