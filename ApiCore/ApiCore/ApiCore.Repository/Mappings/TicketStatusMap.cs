using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class TicketStatusMap : EntityMap<TicketStatus>
    {
        public TicketStatusMap() : base("estado_ticket")
        {
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            
        }

    }
}

