using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ConsortiumMap : EntityMap<Consortium>
    {
        public ConsortiumMap() : base("Consorcio")
        {        
            this.Property(x => x.FriendlyName).IsRequired().HasColumnName("friendly_name");
            this.Property(x => x.CUIT).IsRequired().HasColumnName("cuit");
            this.Property(x => x.MailingList).IsRequired().HasColumnName("mailing_list");            
            this.HasRequired(x => x.Ownership).WithMany().Map(x => x.MapKey("ownership_id"));
            this.HasRequired(x => x.Administration).WithMany().Map(x => x.MapKey("administration_id"));
        }

    }
}

