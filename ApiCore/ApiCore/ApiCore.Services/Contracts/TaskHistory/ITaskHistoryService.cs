using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ApiCore.Services.Contracts.TaskHistory
{
    public interface ITaskHistoryService
    {
        DomainModel.TaskHistory CreateTaskHistory(TaskHistoryRequest TaskHistory);
        DomainModel.TaskHistory GetById(int TaskHistoryId);                
        DomainModel.TaskHistory UpdateTaskHistory(DomainModel.TaskHistory originalTaskHistory, TaskHistoryRequest TaskHistory);
        void DeleteTaskHistory(int TaskHistoryId);
    }
}
