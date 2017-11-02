using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class FunctionalUnitRequest
    {
        public virtual int Id { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual int Floor { get; set; }
        public virtual int Dto { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual int RenterId { get; set; }
    }
}
