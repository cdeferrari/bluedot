using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Ownership
    {
        public virtual int Id { get; set; }
        public virtual Address Address { get; set; }        
        public virtual string Category { get; set; }
        public virtual List<FunctionalUnit> FunctionalUnits { get; set; }
        public virtual List<CommonData> CommonData { get; set; }
        public virtual List<Multimedia> Multimedia { get; set; }
    }
}
