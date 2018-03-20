using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Income
    {
        public virtual Consortium Consortium { get; set; }
        public virtual IncomeType Type { get; set; }
        public virtual DateTime IncomeDate { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string Description { get; set; }
    }
}
