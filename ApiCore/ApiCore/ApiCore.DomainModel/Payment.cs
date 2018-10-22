using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Payment : Entity
    {
        public virtual PaymentType PaymentType { get; set; }
        public virtual AccountStatus AccountStatus { get; set; }

    }
}
