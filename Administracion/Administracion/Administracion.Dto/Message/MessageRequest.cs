using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Message
{
    public class MessageRequest
    {
        public virtual int TicketId { get; set; }
        public virtual int SenderId { get; set; }
        public virtual int? ReceiverId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Content { get; set; }


    }
}
