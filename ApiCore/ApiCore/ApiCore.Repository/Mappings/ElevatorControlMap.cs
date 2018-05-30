using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ElevatorControlMap : EntityMap<ElevatorControl>
    {
        public ElevatorControlMap() : base("control_ascensor")
        {            
            this.Property(x => x.ExpirationDate).IsRequired().HasColumnName("expire_date");
            this.Property(x => x.ControlDate).IsRequired().HasColumnName("control_date");
            this.HasRequired(x => x.Provider).WithMany().Map(x => x.MapKey("provider_id"));         
        }

    }
}

