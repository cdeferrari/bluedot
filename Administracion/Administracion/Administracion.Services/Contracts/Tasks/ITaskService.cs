using Administracion.DomainModel;
using Administracion.Dto.Message;
using Administracion.Dto.Task;
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
        bool CreateTaskHistory(TaskHistoryRequest Task);
        bool DeleteTask(int TaskId);
        bool UpdateTask(TaskRequest task);
        IList<Task> GetByTicketId(int ticketId);
    }
}
