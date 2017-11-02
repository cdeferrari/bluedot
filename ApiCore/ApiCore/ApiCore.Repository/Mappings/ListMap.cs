using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ListMap : EntityMap<List>
    {
        public ListMap() : base("Lista")
        {
            
            this.Property(x => x.Customer).IsRequired().HasColumnName("customer");
            this.Property(x => x.Coments).IsOptional().HasColumnName("coments");
            this.Property(x => x.OpenDate).IsRequired().HasColumnName("open_date");
            this.HasRequired(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));

            this.HasMany(x => x.Tasks).WithRequired(x => x.List).Map(x => x.MapKey("list_id"));

        }

    }
}

