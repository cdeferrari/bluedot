using Administracion.DomainModel;
using Administracion.Dto.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administracion.Services.Contracts.Tasks
{
    public interface ITaskService
    {
        IList<Task> GetAll();
        Task GetTask(int TaskId);
        Entidad CreateTask(TaskRequest Task);        
        bool DeleteTask(int TaskId);
        IList<Task> GetByTicketId(int ticketId);
    }
}
