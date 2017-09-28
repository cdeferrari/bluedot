
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.Priorities;

namespace Administracion.Services.Implementations.Priorities
{
    public class PriorityService : IPriorityService
    {
        public ISync IntegrationService { get; set; }
        
        public IList<Priority> GetAll()
        {
            return IntegrationService.RestCall<List<Priority>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllPriorities, RestMethod.Get, null, null);                                    
        }
      
    }
}
