using ApiCore.DomainModel;
using System.Collections.Generic;


namespace ApiCore.Services.Contracts.TicketStatus
{
    public interface IItemsService
    {
        
        IList<Item> GetAll();
        
    }
}
