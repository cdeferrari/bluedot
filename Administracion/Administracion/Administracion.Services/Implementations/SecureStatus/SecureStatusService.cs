using System.Collections.Generic;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.SecureStatus;

namespace Administracion.Services.Implementations.TaskResult
{
    public class SecureStatusService : ISecureStatusService
    {
        public ISync IntegrationService { get; set; }
        
        public IList<DomainModel.SecureStatus> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.SecureStatus>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllSecureStatus, RestMethod.Get, null, null);                                    
        }
      
    }
}
