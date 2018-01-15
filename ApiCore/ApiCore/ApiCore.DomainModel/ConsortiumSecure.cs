using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class ConsortiumSecure : Entity
    {
        public virtual string PolizyNumber { get; set; }
        public virtual string Brand { get; set; }       
        public virtual Consortium Consortium { get; set; }
        public virtual SecureStatus Status { get; set; }
        public virtual DateTime LimitDate { get; set; }
        
    }
}
