using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class UnitConfiguration
    {
        public virtual FunctionalUnit Unit { get; set; }
        public virtual UnitConfigurationType Type { get; set; }
        public virtual DateTime ConfigurationDate { get; set; }
        public virtual decimal Value { get; set; }
        public virtual string Description { get; set; }
    }
}
