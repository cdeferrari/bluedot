using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Provider
    {
        public virtual int Id { get; set; }
        public virtual string Item { get; set; }
        public virtual User User { get; set; }

        public virtual Address Address { get; set; }
    }
}
