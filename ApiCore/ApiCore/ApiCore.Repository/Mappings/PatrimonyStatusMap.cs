using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class PatrimonyStatusMap : EntityMap<PatrimonyStatus>
    {
        public PatrimonyStatusMap() : base("estado_patrimonial")
        {
            this.Property(x => x.Activo).IsRequired().HasColumnName("activo");
            this.Property(x => x.Pasivo).IsRequired().HasColumnName("pasivo");
            this.Property(x => x.Debe).IsRequired().HasColumnName("debe");
            this.Property(x => x.Haber).IsRequired().HasColumnName("haber");
            this.Property(x => x.StatusDate).IsRequired().HasColumnName("status_date");                        
            this.HasRequired(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));            
            
        }

    }
}

