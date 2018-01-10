using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Ticket : Entity
    {
        public virtual string Customer { get; set; }
        public virtual string Description { get; set; }
        public virtual string Title { get; set; }
        public virtual Consortium Consortium { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
        public virtual DateTime LimitDate { get; set; }
        public virtual FunctionalUnit FunctionalUnit { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual BacklogUser Creator { get; set; }
        public virtual IList<Message> Messages { get; set; }

    }
}
