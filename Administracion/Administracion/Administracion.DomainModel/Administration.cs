using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Administration
    {
        public virtual int Id { get; set; }
        public virtual Address Address { get; set; }        
        public virtual string Name { get; set; }
        public virtual string CUIT { get; set; }
    }
}
