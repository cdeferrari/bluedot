using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class UnitResponse
    {
        public virtual int Id { get; set; }
        public virtual OwnershipUnitResponse Ownership { get; set; }
        public virtual int Floor { get; set; }
        public virtual string Dto { get; set; }
        public virtual int Number { get; set; }
        //public virtual int OwnerId { get; set; }
        //public virtual int RenterId { get; set; }

    }
}
