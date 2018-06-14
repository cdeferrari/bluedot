using Administracion.Services.Contracts.Tasks;
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Dto.Message;
using Administracion.Dto.Task;

namespace Administracion.Services.Implementations.Tasks
{
    public class TaskService : ITaskService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateTask(TaskRequest Task)
        {
          return  IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateTask, RestMethod.Post, null, Task);                        
        }

        public bool UpdateTask(TaskRequest Task)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateTask, Task.Id), RestMethod.Put,  null, Task);                        
        }

        public bool DeleteTask(int TaskId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteTask, TaskId), RestMethod.Delete, null, new RestParamList { new RestParam("id", TaskId) });                        
        }

        public Task GetTask(int TaskId)
        {
            return IntegrationService.RestCall<Task>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetTask, TaskId.ToString()), RestMethod.Get, null, null);                        
            
        }

        public IList<Task> GetAll()
        {
            return IntegrationService.RestCall<List<Task>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllTask, RestMethod.Get, null, null);                                    
        }

        public IList<Task> GetByTicketId(int ticketId)
        {
            return IntegrationService.RestCall<List<Task>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetByTicketId, ticketId), RestMethod.Get, null, null);
        }

        public bool CreateTaskHistory(TaskHistoryRequest Task)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateTaskHistory, RestMethod.Post, null, Task);            
        }

        
    }
}
