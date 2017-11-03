using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Owner : Entity
    {
        public virtual User User {get; set;}  
        public virtual int? FunctionalUnitId { get; set; }      
    }   
}
