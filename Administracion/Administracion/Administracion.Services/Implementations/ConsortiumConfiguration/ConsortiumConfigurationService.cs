using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.ConsortiumConfigurations;
using System;
using Administracion.Dto.ConsortiumConfigurations;

namespace Administracion.Services.Implementations.ConsortiumConfigurations
{
    public class ConsortiumConfigurationService : IConsortiumConfigurationService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateConsortiumConfiguration(ConsortiumConfigurationRequest ConsortiumConfiguration)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateConsortiumConfiguration, RestMethod.Post, null, ConsortiumConfiguration);                        
        }

        public bool UpdateConsortiumConfiguration(ConsortiumConfigurationRequest ConsortiumConfiguration)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateConsortiumConfiguration, ConsortiumConfiguration.Id), RestMethod.Put, null, ConsortiumConfiguration);
        }

        public bool DeleteConsortiumConfiguration(int ConsortiumConfigurationId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteConsortiumConfiguration, ConsortiumConfigurationId), RestMethod.Delete, null, new RestParamList { new RestParam("id", ConsortiumConfigurationId) });                        
        }

        public ConsortiumConfiguration GetConsortiumConfiguration(int ConsortiumConfigurationId)
        {

            return IntegrationService.RestCall<ConsortiumConfiguration>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetConsortiumConfiguration, ConsortiumConfigurationId), RestMethod.Get, null, new RestParamList { new RestParam("id", ConsortiumConfigurationId) });
            
        }

        public List<ConsortiumConfiguration> GetAll()
        {
            return IntegrationService.RestCall<List<ConsortiumConfiguration>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllConsortiumConfigurations, RestMethod.Get, null, null);            
        }

        public IList<ConsortiumConfiguration> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            return IntegrationService.RestCall<List<ConsortiumConfiguration>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetConfigurationByConsortiumId, consortiumId, startDate, endDate), RestMethod.Get, null, null);
        }
    }
}
