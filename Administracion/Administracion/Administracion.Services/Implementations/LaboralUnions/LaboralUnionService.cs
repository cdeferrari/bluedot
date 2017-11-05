
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.LaboralUnion;

namespace Administracion.Services.Implementations.LaboralUnion
{
    public class LaboralUnionService : ILaboralUnionService
    {
        public ISync IntegrationService { get; set; }
        
        public IList<DomainModel.LaboralUnion> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.LaboralUnion>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllLaboralUnion, RestMethod.Get, null, null);                                    
        }
      
    }
}
