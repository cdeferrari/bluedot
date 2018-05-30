using ApiCore.Dtos.Request;
using System.Collections.Generic;

namespace ApiCore.Services.Contracts.TicketHistory
{
    public interface ITicketHistoryService
    {
        DomainModel.TicketHistory CreateTicketHistory(TicketHistoryRequest TicketHistory);
        DomainModel.TicketHistory GetById(int TicketHistoryId);
        IList<DomainModel.TicketHistory> GetAll();        
        DomainModel.TicketHistory UpdateTicketHistory(DomainModel.TicketHistory originalTicketHistory, TicketHistoryRequest TicketHistory);
        void DeleteTicketHistory(int TicketHistoryId);
    }
}
