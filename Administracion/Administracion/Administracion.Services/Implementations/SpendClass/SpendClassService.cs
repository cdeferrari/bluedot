using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.SpendClass;

namespace Administracion.Services.Implementations.SpendClass
{
    public class SpendClassService : ISpendClassService
    {
        public ISync IntegrationService { get; set; }
        
        public List<DomainModel.SpendClass> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.SpendClass>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllSpendClass, RestMethod.Get, null, null);            
        }
        
    }
}
