using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.PatrimonyStatuss;
using Administracion.Dto.PatrimonyStatus;

namespace Administracion.Services.Implementations.PatrimonyStatuss
{
    public class PatrimonyStatusService : IPatrimonyStatusService
    {
        public ISync IntegrationService { get; set; }

        public bool CreatePatrimonyStatus(PatrimonyStatusRequest PatrimonyStatus)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreatePatrimonyStatus, RestMethod.Post, null, PatrimonyStatus);                        
        }

        public bool UpdatePatrimonyStatus(PatrimonyStatusRequest PatrimonyStatus)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdatePatrimonyStatus, PatrimonyStatus.Id), RestMethod.Put, null, PatrimonyStatus);
        }

        public bool DeletePatrimonyStatus(int PatrimonyStatusId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeletePatrimonyStatus, PatrimonyStatusId), RestMethod.Delete, null, new RestParamList { new RestParam("id", PatrimonyStatusId) });                        
        }

        public PatrimonyStatus GetPatrimonyStatus(int PatrimonyStatusId)
        {

            return IntegrationService.RestCall<PatrimonyStatus>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetPatrimonyStatus, PatrimonyStatusId), RestMethod.Get, null, new RestParamList { new RestParam("id", PatrimonyStatusId) });
            
        }

        public List<PatrimonyStatus> GetAll()
        {
            return IntegrationService.RestCall<List<PatrimonyStatus>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllPatrimonyStatus, RestMethod.Get, null, null);            
        }

        public IList<PatrimonyStatus> GetByConsortiumId(int consortiumId)
        {
            return IntegrationService.RestCall<List<PatrimonyStatus>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetPatrimonyStatusByConsortium, consortiumId), RestMethod.Get, null, null);
        }
    }
}
