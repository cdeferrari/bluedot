using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class TicketResponse
    {
        public virtual int id {get; set;}
        public virtual string Customer { get; set; }
        public virtual ConsortiumResponse Consortium { get; set; }        
        public virtual TicketStatusResponse Status { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
        public virtual DateTime LimitDate { get; set; }
        public virtual FunctionalUnit FunctionalUnit { get; set; }
        public virtual PriorityResponse Priority { get; set; }
        public virtual BacklogUserResponse Worker { get; set; }
        public virtual BacklogUserResponse Creator { get; set; }

    }
}
