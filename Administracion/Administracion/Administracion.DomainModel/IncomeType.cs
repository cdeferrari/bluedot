using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class IncomeType
    {
        public virtual string Description { get; set; }
        public virtual Consortium Consortium { get; set; }
        public virtual bool Required { get; set; }
        public virtual int Id { get; set; }
    }
}
