using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.TaskHistory;


namespace ApiCore.Services.Implementations.TaskHistory
{
    public class TaskHistoryService : ITaskHistoryService
    {
        public ITaskHistoryRepository TaskHistoryRepository { get; set; }
        public ITaskRepository TaskRepository { get; set; }

        [Transaction]
        public DomainModel.TaskHistory CreateTaskHistory(TaskHistoryRequest TaskHistory)
        {
            var entityToInsert = new DomainModel.TaskHistory() { };
            MergeTaskHistory(entityToInsert, TaskHistory);
            TaskHistoryRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public DomainModel.TaskHistory GetById(int TaskHistoryId)
        {
            var TaskHistory = TaskHistoryRepository.GetById(TaskHistoryId);
            if (TaskHistory == null)
                throw new BadRequestException(ErrorMessages.TaskHistoryNoEncontrado);

            return TaskHistory;
        }


        [Transaction]
        public DomainModel.TaskHistory UpdateTaskHistory(DomainModel.TaskHistory originalTaskHistory, TaskHistoryRequest TaskHistory)
        {
            this.MergeTaskHistory(originalTaskHistory, TaskHistory);
            TaskHistoryRepository.Update(originalTaskHistory);
            return originalTaskHistory;

        }


        [Transaction]
        public void DeleteTaskHistory(int TaskHistoryId)
        {
            var TaskHistory = TaskHistoryRepository.GetById(TaskHistoryId);
            TaskHistoryRepository.Delete(TaskHistory);
        }

        [Transaction]
        public IList<DomainModel.TaskHistory> GetAll()
        {
            var TaskHistorys = TaskHistoryRepository.GetAll();
            if (TaskHistorys == null)
                throw new BadRequestException(ErrorMessages.TaskHistoryNoEncontrado);

            var result = new List<DomainModel.TaskHistory>();
            var enumerator = TaskHistorys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }


        private void MergeTaskHistory(DomainModel.TaskHistory originalTaskHistory, TaskHistoryRequest TaskHistory)
        {
            originalTaskHistory.Coment = TaskHistory.Coment;
            originalTaskHistory.FollowDate = TaskHistory.FollowDate;
            originalTaskHistory.Task = this.TaskRepository.GetById(TaskHistory.TaskId);            
        }
        
    }

}
