using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Province : Entity
    {
        public virtual string Description { get; set; }        
        public virtual Country Country { get; set; }
        public virtual IList<City> Cities { get; set; }

    }
}
