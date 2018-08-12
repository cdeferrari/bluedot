using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.UnitConfigurations;
using System;
using Administracion.Dto.UnitConfigurations;

namespace Administracion.Services.Implementations.UnitConfigurations
{
    public class UnitConfigurationService : IUnitConfigurationService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateUnitConfiguration(UnitConfigurationRequest UnitConfiguration)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateUnitConfiguration, RestMethod.Post, null, UnitConfiguration);                        
        }

        
        public bool DeleteUnitConfiguration(int UnitConfigurationId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteUnitConfiguration, UnitConfigurationId), RestMethod.Delete, null, new RestParamList { new RestParam("id", UnitConfigurationId) });                        
        }

        public UnitConfiguration GetUnitConfiguration(int UnitConfigurationId)
        {

            return IntegrationService.RestCall<UnitConfiguration>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetUnitConfiguration, UnitConfigurationId), RestMethod.Get, null, new RestParamList { new RestParam("id", UnitConfigurationId) });
            
        }
        
        public IList<UnitConfiguration> GetByUnitId(int UnitId, DateTime startDate, DateTime endDate)
        {
            var sstartDate = startDate.Date.ToShortDateString();
            var sendDate = endDate.Date.ToShortDateString();
            return IntegrationService.RestCall<List<UnitConfiguration>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetConfigurationByUnitId, UnitId, sstartDate, sendDate), RestMethod.Get, null, null);
        }
    }
}
