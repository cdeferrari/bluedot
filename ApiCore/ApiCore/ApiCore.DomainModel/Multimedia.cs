using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Multimedia : Entity
    {
         public virtual string Url { get; set; }
         public virtual int MultimediaTypeId { get; set; }
        public virtual Ownership Ownership { get; set; }

    }
}
