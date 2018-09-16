using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class AccountStatus : Entity
    {
        public virtual DateTime StatusDate { get; set; }
        public virtual FunctionalUnit Unit { get; set; }
        public virtual decimal Debe { get; set; }
        public virtual decimal Haber { get; set; }        
    }
}
