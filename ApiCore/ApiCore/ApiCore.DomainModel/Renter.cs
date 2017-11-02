using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Renter : Entity
    {
        public virtual User User {get; set;}
        public virtual int PaymentTypeId { get; set; }
        public virtual FunctionalUnit FunctionalUnit { get; set; }
    }   
}
