using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class FunctionalUnit : Entity
    {
        public virtual Ownership Ownership {get; set;}
        public virtual int Floor { get; set; }
        public virtual string Dto { get; set; }
    }
}
