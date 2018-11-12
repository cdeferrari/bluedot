using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class AccountStatusMap : EntityMap<AccountStatus>
    {
        public AccountStatusMap() : base("estado_cuenta")
        {
            this.Property(x => x.Debe).IsRequired().HasColumnName("debe");
            this.Property(x => x.Haber).IsRequired().HasColumnName("haber");
            this.Property(x => x.StatusDate).IsRequired().HasColumnName("status_date");                        
            this.Property(x => x.Interest).IsRequired().HasColumnName("interes");                        
            this.HasRequired(x => x.Unit).WithMany().Map(x => x.MapKey("unit_id"));            
            
        }

    }
}

