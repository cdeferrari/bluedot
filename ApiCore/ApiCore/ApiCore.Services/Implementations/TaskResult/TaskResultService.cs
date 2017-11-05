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

namespace ApiCore.Services.Implementations.TaskResult
{
    public class TaskResultService : ITaskResultService
    {
        public ITaskResultRepository TaskResultRepository { get; set; }
        
        
        [Transaction]
        public IList<DomainModel.TaskResult> GetAll()
        {
            var status = TaskResultRepository.GetAll();
            if (status == null)
                throw new BadRequestException(ErrorMessages.TicketNoEncontrado);

            var result = new List<DomainModel.TaskResult>();
            var enumerator = status.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

    }
}
