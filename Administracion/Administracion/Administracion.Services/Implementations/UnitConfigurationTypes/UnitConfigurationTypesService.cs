
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Dto;
using Administracion.Services.Contracts.UnitConfigurationTypes;

namespace Administracion.Services.Implementations.UnitConfigurationTypes
{
    public class UnitConfigurationTypesService : IUnitConfigurationTypeService
    {
        public ISync IntegrationService { get; set; }
        
        
        public List<UnitConfigurationType> GetAll()
        {
            return IntegrationService.RestCall<List<UnitConfigurationType>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllUnitConfigurationTypes, RestMethod.Get, null, null);                                    
        }
        
       
    }
}
