using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Administration : Entity
    {
         public virtual string Name { get; set; }
         public virtual string CUIT { get; set; }
         public virtual string DirectionId { get; set; }
         public virtual DateTime StartDate { get; set; }         

    }
}
