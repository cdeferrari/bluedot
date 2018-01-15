
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.Provinces;
using Administracion.Dto.City;

namespace Administracion.Services.Implementations.Province
{
    public class CityService : ICityService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateCity(CityRequest city)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateCity, RestMethod.Post, null, city);
        }

        public bool DeleteCity(int id)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteCity, id), RestMethod.Delete, null, new RestParamList { new RestParam("id", id) });
        }

        public IList<City> GetAll()
        {
            return IntegrationService.RestCall<List<City>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllCities, RestMethod.Get, null, null);                                    
        }
      
    }
}
