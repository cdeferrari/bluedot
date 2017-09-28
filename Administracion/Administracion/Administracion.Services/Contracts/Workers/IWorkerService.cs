using Administracion.DomainModel;
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
        bool CreateWorker(Worker Worker);
        bool UpdateWorker(Worker Worker);
        bool DeleteWorker(int WorkerId);
    }
}
