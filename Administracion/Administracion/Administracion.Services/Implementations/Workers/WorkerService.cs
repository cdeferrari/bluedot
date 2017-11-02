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
using Administracion.Dto.Worker;

namespace Administracion.Services.Implementations.Workers
{
    public class WorkerService : IWorkerService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateWorker(WorkerRequest Worker)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateWorker, RestMethod.Post, null, Worker);                        
        }

        public bool UpdateWorker(Worker Worker)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateWorker, Worker.Id), RestMethod.Put, null, Worker);                        
        }

        public bool DeleteWorker(int WorkerId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteWorker, WorkerId), RestMethod.Delete, null, new RestParamList { new RestParam("id", WorkerId) });                        
        }

        public Worker GetWorker(int WorkerId)
        {
            return IntegrationService.RestCall<Worker>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetWorker, WorkerId) , RestMethod.Get, null, new RestParamList { new RestParam("id", WorkerId) });                        
            
        }

        public IList<Worker> GetAll()
        {
            return IntegrationService.RestCall<List<Worker>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllWorkers, RestMethod.Get, null, null);                                    
        }
      
    }
}
