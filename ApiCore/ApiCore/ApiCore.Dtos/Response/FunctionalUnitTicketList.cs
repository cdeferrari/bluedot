using ApiCore.DomainModel;
using System.Collections.Generic;

namespace ApiCore.Dtos.Response
{
    public class FunctionalUnitTicketList
    {
        public virtual int Id { get; set; }
        public virtual int Floor { get; set; }
        public virtual string Dto { get; set; }
        public virtual int Number { get; set; }

    }
}
