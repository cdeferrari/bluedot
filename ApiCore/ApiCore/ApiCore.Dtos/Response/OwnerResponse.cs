using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class OwnerResponse
    {
        public virtual int Id { get; set; }
        public virtual UserResponse User { get; set; }        
        public virtual List<int> FunctionalUnitId { get; set; }
        public virtual int? PaymentTypeId { get; set; }
    }
}
