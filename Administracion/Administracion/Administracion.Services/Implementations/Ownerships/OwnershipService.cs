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
using Administracion.Services.Contracts.Ownerships;

namespace Administracion.Services.Implementations.Ownerships
{
    public class OwnershipService : IOwnershipService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateOwnership(Ownership Ownership)
        {
            return IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateOwnership, RestMethod.Post, null, Ownership);                        
        }

        public bool UpdateOwnership(Ownership Ownership)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateOwnership, Ownership.Id), RestMethod.Put, null,Ownership);                        
        }

        public bool DeleteOwnership(int OwnershipId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteOwnership, OwnershipId), RestMethod.Delete, null, new RestParamList { new RestParam("id", OwnershipId) });                        
        }

        public Ownership GetOwnership(int OwnershipId)
        {

            return IntegrationService.RestCall<Ownership>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetOwnership, OwnershipId), RestMethod.Get, null, new RestParamList { new RestParam("id", OwnershipId) });
            
        }

        public IList<Ownership> GetAll()
        {

            return IntegrationService.RestCall<List<Ownership>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllOwnerships, RestMethod.Get, null, null);

        }
    }
}
