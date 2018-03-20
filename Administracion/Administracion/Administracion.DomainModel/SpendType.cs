using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class SpendType
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual SpendItem Item { get; set; }
        public virtual Consortium Consortium { get; set; }
        public virtual bool Required { get; set; }
    }
}
