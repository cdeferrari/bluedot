using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class CommonData : Entity
    {
        public virtual CommonDataItem Item { get; set; }
        public virtual Ownership Ownership { get; set; }
        public virtual bool Have { get; set; }
    }
}
