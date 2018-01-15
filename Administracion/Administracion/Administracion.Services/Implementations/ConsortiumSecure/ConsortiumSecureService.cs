using Administracion.Services.Contracts.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.ConsortiumSecure;
using Administracion.Dto.ConsortiumSecure;

namespace Administracion.Services.Implementations.ConsortiumSecure
{
    public class ConsortiumSecureService : IConsortiumSecureService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateConsortiumSecure(ConsortiumSecureRequest ConsortiumSecure)
        {
          return IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateConsortiumSecure, RestMethod.Post, null, ConsortiumSecure);                        
        }

        public bool UpdateConsortiumSecure(ConsortiumSecureRequest ConsortiumSecure)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateConsortiumSecure, ConsortiumSecure.Id), RestMethod.Put, null, ConsortiumSecure);                        
        }

        public bool DeleteConsortiumSecure(int ConsortiumSecureId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format( ApiCore.DeleteConsortiumSecure, ConsortiumSecureId), RestMethod.Delete, null, new RestParamList { new RestParam("id", ConsortiumSecureId) });                        
        }

        public DomainModel.ConsortiumSecure GetConsortiumSecure(int ConsortiumSecureId)
        {
            return IntegrationService.RestCall<DomainModel.ConsortiumSecure>(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.GetConsortiumSecure, ConsortiumSecureId), RestMethod.Get, null, new RestParamList { new RestParam("id", ConsortiumSecureId) });                        
            
        }

        public IList<DomainModel.ConsortiumSecure> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.ConsortiumSecure>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllConsortiumSecure, RestMethod.Get, null, null);                                    
        }
      
    }
}
