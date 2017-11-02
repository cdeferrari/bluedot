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
using Newtonsoft.Json;
using Administracion.Services.Contracts.Providers;
using Administracion.Dto.Provider;

namespace Administracion.Services.Implementations.Providers
{
    public class ProviderService : IProviderService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateProvider(ProviderRequest Provider)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateProvider, RestMethod.Post, null, Provider);                        
        }

        public bool UpdateProvider(ProviderRequest Provider)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateProvider, Provider.Id), RestMethod.Put, null, Provider);                        
        }

        public bool DeleteProvider(int ProviderId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteProvider, ProviderId), RestMethod.Delete, null, new RestParamList { new RestParam("id", ProviderId) });                        
        }

        public Provider GetProvider(int ProviderId)
        {
            return IntegrationService.RestCall<Provider>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetProvider, ProviderId) , RestMethod.Get, null, new RestParamList { new RestParam("id", ProviderId) });                        
            
        }

        public IList<Provider> GetAll()
        {
            return IntegrationService.RestCall<List<Provider>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllProviders, RestMethod.Get, null, null);                                    
        }
      
    }
}
