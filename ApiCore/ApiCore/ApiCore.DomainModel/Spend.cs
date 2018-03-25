using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Spend : Entity
    {
        public virtual Bill Bill {get; set;}
        public virtual Consortium Consortium { get; set; }
        public virtual SpendType Type { get; set; }
        public virtual DateTime PaymentDate { get; set; }
        public virtual string Description { get; set; }
        public virtual Task Task { get; set; }
    }
}
