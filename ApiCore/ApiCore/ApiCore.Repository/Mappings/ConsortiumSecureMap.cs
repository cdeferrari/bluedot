using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ConsortiumSecureMap : EntityMap<ConsortiumSecure>
    {
        public ConsortiumSecureMap() : base("seguro_consorcio")
        {
            
            this.Property(x => x.PolizyNumber).IsRequired().HasColumnName("polizy_number");
            
            this.Property(x => x.Brand).IsRequired().HasColumnName("brand");
            this.Property(x => x.LimitDate).IsRequired().HasColumnName("limit_date");
            //this.HasRequired(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));
            this.HasRequired(x => x.Status).WithMany().Map(x => x.MapKey("secure_status_id"));
            

        }

    }
}

