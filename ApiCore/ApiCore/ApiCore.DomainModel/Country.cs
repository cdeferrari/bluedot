using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Country : Entity
    {
        public virtual string Description { get; set; }         
    }
}
