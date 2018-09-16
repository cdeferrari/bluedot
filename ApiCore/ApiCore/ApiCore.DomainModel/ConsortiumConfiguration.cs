using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class ConsortiumConfiguration : Entity
    {     
        public virtual Consortium Consortium { get; set; }
        public virtual ConsortiumConfigurationType Type { get; set; }
        public virtual DateTime ConfigurationDate { get; set; }
        public virtual decimal Value { get; set; }        
    }
}
