using ApiCore.Services.Contracts.Tasks;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Tasks
{
    public class TaskService : ITaskService
    {
        public ITaskRepository TaskRepository { get; set; }
        public ITicketRepository TicketRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }        
        public IStatusRepository StatusRepository { get; set; }
        public IFunctionalUnitRepository FunctionalUnitRepository { get; set; }
        public IPriorityRepository PriorityRepository { get; set; }        
        public IBacklogUserRepository BacklogUserRepository { get; set; }
        public IWorkerRepository WorkerRepository { get; set; }
        public IProviderRepository ProviderRepository { get; set; }
        public IManagerRepository ManagerRepository { get; set; }

        [Transaction]
        public Task CreateTask(TaskRequest Task)
        {
            var entityToInsert = new Task() { };
            MergeTask(entityToInsert, Task);
            TaskRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Task GetById(int TaskId)
        {
            var Task = TaskRepository.GetById(TaskId);
            if (Task == null)
                throw new BadRequestException(ErrorMessages.TareaNoEncontrada);

            return Task;
        }
        

        [Transaction]
        public Task UpdateTask(Task originalTask, TaskRequest Task)
        {            
            this.MergeTask(originalTask, Task);
            TaskRepository.Update(originalTask);
            return originalTask;

        }
        

        [Transaction]
        public void DeleteTask(int TaskId)
        {
            var Task = TaskRepository.GetById(TaskId);
            TaskRepository.Delete(Task);
        }

        [Transaction]
        public IList<Task> GetAll()
        {
            var Tasks = TaskRepository.GetAll();
            if (Tasks == null)
                throw new BadRequestException(ErrorMessages.TareaNoEncontrada);

            var result = new List<Task>();
            var enumerator = Tasks.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

        private void MergeTask(Task originalTask, TaskRequest Task)
        {
            
            originalTask.Ticket =  this.TicketRepository.GetById(Task.TicketId);
            originalTask.Status = this.StatusRepository.GetById(Task.StatusId);
            originalTask.OpenDate = Task.OpenDate;            
            originalTask.Priority = this.PriorityRepository.GetById(Task.PriorityId);        
            originalTask.Creator = this.BacklogUserRepository.GetById(Task.CreatorId);            
            originalTask.Description = Task.Description;

            if (Task.WorkerId.HasValue)
            {
                originalTask.Worker = this.WorkerRepository.GetById(Task.WorkerId.Value);
            }

            if (Task.ProviderId.HasValue)
            {
                originalTask.Provider = this.ProviderRepository.GetById(Task.ProviderId.Value);
            }
            if (Task.ManagerId.HasValue)
            {
                originalTask.Manager = this.ManagerRepository.GetById(Task.ManagerId.Value);
            }

            if (Task.CloseDate.HasValue)
            {
                originalTask.CloseDate =  Task.CloseDate.Value;
            }
            
            
        }

        public IList<Task> GetByTicketId(int ticketId)
        {
            return TaskRepository.GetByTicketId(ticketId).ToList();
            
        }
    }
}
