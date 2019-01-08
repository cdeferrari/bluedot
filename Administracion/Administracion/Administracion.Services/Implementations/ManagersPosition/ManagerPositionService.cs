using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.ManagerPositions;

namespace Administracion.Services.Implementations.Status
{
    public class ManagerPositionService : IManagerPositionService
    {
        public ISync IntegrationService { get; set; }
        
        //public bool DeleteManagerPosition(int id)
        //{
        //    return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteManagerPosition, id), RestMethod.Delete, null, new RestParamList { new RestParam("id", id) });
        //}

        public IList<ManagerPosition> GetAll()
        {
            return IntegrationService.RestCall<List<ManagerPosition>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllManagerPosition, RestMethod.Get, null, null);                                    
        }
        
    }
}
