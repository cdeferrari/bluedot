using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class TaskResponse
    {
        public virtual int Id {get; set;}
        public virtual TicketStatusResponse Status { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
        public virtual PriorityResponse Priority { get; set; }
        public virtual WorkerResponse Worker { get; set; }
        public virtual ManagerResponse Manager { get; set; }
        public virtual ProviderResponse Provider { get; set; }
        public virtual BacklogUserResponse Creator { get; set; }        
        public virtual string Description { get; set; }
        
        public virtual IList<Spend> Spends { get;set; }
        public virtual IList<TaskHistoryResponse> TaskHistory { get; set; }
    }
}
