using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class FireExtinguisherControl : Entity
    {
        public virtual Consortium Consortium { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime ControlDate { get; set; }
    }
}
