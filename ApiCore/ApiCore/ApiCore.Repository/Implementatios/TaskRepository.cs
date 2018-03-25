using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Repository.Implementatios
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public IList<Task> GetByTicketId(int ticketId)
        {
            var result = this.Context.Set<Task>().Where(x => x.Ticket.Id == ticketId)
                .ToList();
            return result;
        }
    }
}
