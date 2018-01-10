using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Message : Entity
    {        
        public virtual Ticket Ticket { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public virtual string Content { get; set; }                
        public virtual DateTime Date { get; set; }
        
    }
}
