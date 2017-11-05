
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.TaskResult;

namespace Administracion.Services.Implementations.TaskResult
{
    public class TaskResultService : ITaskResultService
    {
        public ISync IntegrationService { get; set; }
        
        public IList<DomainModel.TaskResult> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.TaskResult>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllTaskResult, RestMethod.Get, null, null);                                    
        }
      
    }
}
