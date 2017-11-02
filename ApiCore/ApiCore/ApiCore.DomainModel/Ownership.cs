
using System.Collections.Generic;

namespace ApiCore.DomainModel
{
    public class Ownership : Entity
    {        
        public virtual Address Address { get; set; }     
        public virtual List<FunctionalUnit> FunctionalUnits { get; set; }
    }
}
