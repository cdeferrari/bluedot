using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class FunctionalUnit
    {
        public virtual int Id { get; set; }
        public virtual Ownership Ownership { get; set; }
        public virtual int Floor { get; set; }
        public virtual string Dto { get; set; }
        public virtual int Number { get; set; }


    }
}
