using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class ConsortiumConfigurationType : Entity
    {
        public virtual string Description {get; set;}
        public virtual bool IsPercentage { get; set; }

        public virtual IList<UnitConfigurationType> UnitConfigurationTypes { get; set; }
    }
}
