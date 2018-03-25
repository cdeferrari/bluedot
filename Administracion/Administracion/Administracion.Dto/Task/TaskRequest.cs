using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Message
{
    public class TaskRequest
    {
        public virtual int Id { get; set; }
        public virtual int TicketId { get; set; }
        public virtual int PriorityId { get; set; }
        public virtual int StatusId { get; set; }
        public virtual string Description { get; set; }
        public virtual int WorkerId { get; set; }
        public virtual int ProviderId { get; set; }
        public virtual int ManagerId { get; set; }
        public virtual int CreatorId { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime? CloseDate { get; set; }

    }
}
