using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.TicketStatus
{
    public interface ITicketStatusService
    {
        
        IList<DomainModel.TicketStatus> GetAll();
        
    }
}
