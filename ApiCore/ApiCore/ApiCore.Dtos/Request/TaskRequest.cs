using System;

namespace ApiCore.Dtos.Request
{
    public class TaskRequest
    {        
        public virtual string Description { get; set; }
        public virtual int StatusId { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime? CloseDate { get; set; }       
        public virtual int PriorityId { get; set; }
        public virtual int TicketId { get; set; }
        public virtual int? WorkerId { get; set; }
        public virtual int? ProviderId { get; set; }
        public virtual int? ManagerId { get; set; }
        public virtual int CreatorId { get; set; }

    }
}
