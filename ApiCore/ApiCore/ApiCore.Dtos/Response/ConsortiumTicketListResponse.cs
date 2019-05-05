using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class ConsortiumTicketListResponse
    {
        public virtual int Id { get; set; }        
        
        public virtual OwnershipTicketListResponse Ownership { get; set; }

        
    }
}
