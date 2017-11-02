using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Provider : Entity
    {
        public virtual User User {get; set;}
        public virtual string Item { get; set; }
        public virtual Address Address { get; set; }
    }   
}
