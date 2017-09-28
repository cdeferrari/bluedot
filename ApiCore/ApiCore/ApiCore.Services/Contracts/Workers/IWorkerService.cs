using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Workers
{
    public interface IWorkerService
    {
        Worker CreateWorker(WorkerRequest Worker);
        Worker GetById(int WorkerId);        
        Worker UpdateWorker(Worker originalWorker, WorkerRequest Worker);
        void DeleteWorker(int WorkerId);
        List<Worker> GetAll();
    }
}
