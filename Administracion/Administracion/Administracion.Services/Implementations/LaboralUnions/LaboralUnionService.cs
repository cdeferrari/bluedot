
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.LaboralUnion;
using Administracion.Dto;

namespace Administracion.Services.Implementations.LaboralUnion
{
    public class LaboralUnionService : ILaboralUnionService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateLaboralUnion(DescriptionRequest laboralUnion)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateLaboralUnion, RestMethod.Post, null, laboralUnion);
        }

        public bool DeleteLaboralUnion(int id)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteLaboralUnion, id), RestMethod.Delete, null, new RestParamList { new RestParam("id", id) });
        }

        public IList<DomainModel.LaboralUnion> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.LaboralUnion>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllLaboralUnion, RestMethod.Get, null, null);                                    
        }
      
    }
}
