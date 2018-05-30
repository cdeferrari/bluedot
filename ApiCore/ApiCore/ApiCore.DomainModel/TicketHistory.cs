using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class TicketHistory : Entity
    {
        public virtual string Coment {get; set;}             
        public virtual DateTime FollowDate { get; set; }
        public Ticket Ticket { get; set; }        
    }
}
