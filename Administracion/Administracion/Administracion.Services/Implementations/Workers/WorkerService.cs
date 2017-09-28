using Administracion.Services.Contracts.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.Workers;

namespace Administracion.Services.Implementations.Workers
{
    public class WorkerService : IWorkerService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateWorker(Worker Worker)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateWorker, RestMethod.Post, null, Worker);                        
        }

        public bool UpdateWorker(Worker Worker)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateWorker, RestMethod.Put, null, new RestParamList { new RestParam("id", Worker.Id) , new RestParam("Worker", Worker) });                        
        }

        public bool DeleteWorker(int WorkerId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteWorker, RestMethod.Delete, null, new RestParamList { new RestParam("id", WorkerId) });                        
        }

        public Worker GetWorker(int WorkerId)
        {
            return IntegrationService.RestCall<Worker>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetWorker, RestMethod.Delete, null, new RestParamList { new RestParam("id", WorkerId) });                        
            
        }

        public IList<Worker> GetAll()
        {
            return IntegrationService.RestCall<List<Worker>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllWorkers, RestMethod.Get, null, null);                                    
        }
      
    }
}
