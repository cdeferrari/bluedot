using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class IncomeType : Entity
    {
        public virtual string Description {get; set;}     
        public virtual Consortium Consortium { get; set; }
        public virtual bool Required { get; set; }
    }
}
