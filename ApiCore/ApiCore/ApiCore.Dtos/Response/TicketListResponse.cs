using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class TicketListResponse
    {
        public virtual int Id {get; set;}
        public virtual string Customer { get; set; }
        public virtual ConsortiumTicketListResponse Consortium { get; set; }        
        public virtual TicketStatusResponse Status { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
        public virtual DateTime LimitDate { get; set; }
        public virtual FunctionalUnitTicketList FunctionalUnit { get; set; }
        public virtual PriorityResponse Priority { get; set; }
        public virtual ManagerResponseTicketList Manager { get; set; }
        public virtual BacklogUserResponseTicketList BacklogUser { get; set; }        
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? Resolved { get; set; }
    }
}
