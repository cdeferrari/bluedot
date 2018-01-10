using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class MessageResponse
    {
        public virtual int Id { get; set; }
        public virtual UserResponse Sender { get; set; }
        public virtual UserResponse Receiver { get; set; }        
        public virtual DateTime Date { get; set; }
        public virtual string Content { get; set; }
        
    }
}
