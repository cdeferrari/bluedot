using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class PaymentMap : EntityMap<Payment>
    {
        public PaymentMap() : base("pago")
        {                        
            
            this.HasRequired(x => x.PaymentType).WithMany().Map(x => x.MapKey("payment_type_id"));
            this.HasRequired(x => x.AccountStatus).WithMany().Map(x => x.MapKey("account_status_id"));

        }

    }
}

