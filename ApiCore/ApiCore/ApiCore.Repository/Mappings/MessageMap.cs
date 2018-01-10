using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class MessageMap : EntityMap<Message>
    {
        public MessageMap() : base("mensaje")
        {
            this.Property(x => x.Date).IsRequired().HasColumnName("date");
            this.HasRequired(x => x.Sender).WithMany().Map(x => x.MapKey("sender_id"));
            this.HasOptional(x => x.Receiver).WithMany().Map(x => x.MapKey("receiver_id"));
            this.Property(x => x.Content).IsRequired().HasColumnName("content");

        }

    }
}

