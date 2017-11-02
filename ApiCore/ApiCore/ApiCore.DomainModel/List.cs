using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class List : Entity
    {                   

        public virtual string Customer { get; set; }
        public virtual Consortium Consortium { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual IList<TaskList> Tasks { get; set; }

        public virtual string Coments { get; set; }
    }
}
