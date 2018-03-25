using ApiCore.DomainModel;
using System.Collections.Generic;

namespace ApiCore.Repository.Contracts
{
    public interface ITaskRepository : IRepository<Task>
    {
        IList<Task> GetByTicketId(int ticketId);
    }
}
