using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System.Collections.Generic;


namespace ApiCore.Services.Contracts.Tasks
{
    public interface ITaskService
    {
        Task CreateTask(TaskRequest Task);
        Task GetById(int TaskId);
        IList<Task> GetAll();
        IList<Task> GetByTicketId(int ticketId);
        Task UpdateTask(Task originalTask, TaskRequest Task);
        void DeleteTask(int TaskId);
    }
}
