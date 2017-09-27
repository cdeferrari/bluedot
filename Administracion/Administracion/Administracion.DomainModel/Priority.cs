using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Priority 
    {
        public virtual string Description { get; set; }
        public virtual int Value { get; set; }
        public virtual int Id { get; set; }
    }
}
