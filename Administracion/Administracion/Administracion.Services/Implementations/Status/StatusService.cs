
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.Status;

namespace Administracion.Services.Implementations.Status
{
    public class StatusService : IStatusService
    {
        public ISync IntegrationService { get; set; }
        
        public IList<DomainModel.Status> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.Status>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllStatus, RestMethod.Get, null, null);                                    
        }
      
    }
}
