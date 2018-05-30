using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.AreaService;

namespace Administracion.Services.Implementations.AreaService
{
    public class AreaService : IAreaService
    {
        public ISync IntegrationService { get; set; }
        

        public IList<Area> GetAll()
        {
            return IntegrationService.RestCall<List<Area>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllAreas, RestMethod.Get, null, null);                                    
        }
      
    }
}
