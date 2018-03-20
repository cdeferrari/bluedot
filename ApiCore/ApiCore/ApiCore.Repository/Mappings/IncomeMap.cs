using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class IncomeMap : EntityMap<Income>
    {
        public IncomeMap() : base("ingreso")
        {
            this.Property(x => x.Amount).IsRequired().HasColumnName("amount");
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
            this.Property(x => x.IncomeDate).IsRequired().HasColumnName("income_date");
            this.HasRequired(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));
            this.HasRequired(x => x.Type).WithMany().Map(x => x.MapKey("income_type_id"));
            
        }

    }
}

