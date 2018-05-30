using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class SpendMap : EntityMap<Spend>
    {
        public SpendMap() : base("gasto")
        {            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
            this.Property(x => x.PaymentDate).IsRequired().HasColumnName("payment_date");
            this.HasRequired(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));
            this.HasRequired(x => x.Type).WithMany().Map(x => x.MapKey("spend_type_id"));
            this.HasOptional(x => x.SpendClass).WithMany().Map(x => x.MapKey("spend_class_id"));
            this.HasRequired(x => x.Bill).WithMany().Map(x => x.MapKey("bill_id"));
            this.HasOptional(x => x.Task).WithMany().Map(x => x.MapKey("task_id"));
        }

    }
}

