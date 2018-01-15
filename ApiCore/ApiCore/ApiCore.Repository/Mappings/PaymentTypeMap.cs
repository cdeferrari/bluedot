using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class PaymentTypeMap : EntityMap<PaymentType>
    {
        public PaymentTypeMap() : base("tipo_pago")
        {            
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

