using ApiCore.DomainModel;
using System.Collections.Generic;

namespace ApiCore.Dtos.Response
{
    public class FunctionalUnitResponse
    {
        public virtual int Floor { get; set; }
        public virtual int Dto { get; set; }
        public virtual UnitOwnerResponse Owner { get; set; }
        public virtual UnitRenterResponse Renter { get; set; }

        public virtual OwnershipUnitResponse Ownership { get; set; }
    }
}
