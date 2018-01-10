using System;

namespace ApiCore.Dtos.Request
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
