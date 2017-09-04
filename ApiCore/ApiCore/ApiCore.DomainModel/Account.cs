using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Account : Entity
    {
        public virtual string CBU { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual int Number { get; set; }
        
    }
}
