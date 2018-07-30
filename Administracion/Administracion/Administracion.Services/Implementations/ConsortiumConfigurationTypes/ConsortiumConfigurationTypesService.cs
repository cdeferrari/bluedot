
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Dto;
using Administracion.Services.Contracts.ConsortiumConfigurationTypes;

namespace Administracion.Services.Implementations.ConsortiumConfigurationTypes
{
    public class ConsortiumConfigurationTypesService : IConsortiumConfigurationTypeService
    {
        public ISync IntegrationService { get; set; }
        
        
        public List<ConsortiumConfigurationType> GetAll()
        {
            return IntegrationService.RestCall<List<ConsortiumConfigurationType>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllConsortiumConfigurationTypes, RestMethod.Get, null, null);                                    
        }

        
        
       
    }
}
