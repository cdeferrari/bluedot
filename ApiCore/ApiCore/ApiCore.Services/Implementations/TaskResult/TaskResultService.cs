using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.TaskResult;
using System.Linq;

namespace ApiCore.Services.Implementations.TaskResult
{
    public class TaskResultService : ITaskResultService
    {
        public ITaskResultRepository TaskResultRepository { get; set; }
        
        
        [Transaction]
        public IList<DomainModel.TaskResult> GetAll()
        {
            return TaskResultRepository.GetAll().ToList();
            
        }
        

    }
}
