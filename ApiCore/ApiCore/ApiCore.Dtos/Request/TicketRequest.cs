using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class TicketRequest
    {

        public virtual string Customer { get; set; }
        public virtual string Description { get; set; }
        public virtual string Title { get; set; }
        public virtual int ConsortiumId { get; set; }        
        public virtual int StatusId { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
        public virtual DateTime LimitDate { get; set; }
        public virtual int FunctionalUnitId { get; set; }
        public virtual int PriorityId { get; set; }
        public virtual int WorkerId { get; set; }
        public virtual int CreatorId { get; set; }        

    }
}
