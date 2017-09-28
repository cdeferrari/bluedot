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
        public virtual Address Address { get; set; }        
    }
}
