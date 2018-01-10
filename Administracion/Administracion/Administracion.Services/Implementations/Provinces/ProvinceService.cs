using System;
using System.Collections.Generic;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.Provinces;
using Administracion.Dto.Province;


namespace Administracion.Services.Implementations.Province
{
    public class ProvinceService : IProvinceService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateProvince(ProvinceRequest Province)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateProvince, RestMethod.Post, null, Province);
        }

        public bool DeleteProvince(int id)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteProvince, id), RestMethod.Delete, null, new RestParamList { new RestParam("id", id) });
        }

        public IList<DomainModel.Province> GetAllProvinces()
        {
            return IntegrationService.RestCall<List<DomainModel.Province>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllProvinces, RestMethod.Get, null, null);                                    
        }
      
    }
}
