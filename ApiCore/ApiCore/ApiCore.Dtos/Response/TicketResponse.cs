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
        public virtual int Id {get; set;}
        public virtual string Customer { get; set; }
        public virtual ConsortiumResponse Consortium { get; set; }        
        public virtual TicketStatusResponse Status { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
        public virtual DateTime LimitDate { get; set; }
        public virtual FunctionalUnit FunctionalUnit { get; set; }
        public virtual PriorityResponse Priority { get; set; }
        public virtual WorkerResponse Worker { get; set; }
        public virtual ManagerResponse Manager { get; set; }
        public virtual BacklogUserResponse BacklogUser { get; set; }
        public virtual BacklogUserResponse Creator { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<MessageResponse> Messages { get; set; }
        public virtual IList<TaskResponse> Tasks { get; set; }
        public virtual Area Area { get; set; }
        public virtual IList<TicketHistoryResponse> TicketHistory { get; set; }
    }
}
