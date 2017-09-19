using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Task : Entity
    {        
        public virtual Ticket Ticket { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual Priority Priority { get; set; }                
        public virtual string Description { get; set; }
        public virtual Date OpenDate { get; set; }
        public virtual Date CloseDate { get; set; }        
        public virtual BacklogUser Worker { get; set; }
        public virtual BacklogUser Creator { get; set; }        

    }
}
