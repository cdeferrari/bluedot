using Administracion.DomainModel;
using Administracion.Dto.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Workers
{
    public interface IWorkerService
    {
        IList<Worker> GetAll();
        Worker GetWorker(int WorkerId);
        bool CreateWorker(WorkerRequest Worker);
        bool UpdateWorker(WorkerRequest Worker);
        bool DeleteWorker(int WorkerId);
    }
}
