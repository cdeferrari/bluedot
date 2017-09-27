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

        public void CreateOwnership(Ownership Ownership)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateOwnership, RestMethod.Post, null, new RestParamList { new RestParam("Ownership", Ownership) });                        
        }

        public void UpdateOwnership(Ownership Ownership)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateOwnership, RestMethod.Put, null, new RestParamList {new RestParam("id", Ownership.Id), new RestParam("Ownership", Ownership) });                        
        }

        public void DeleteOwnership(int OwnershipId)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteOwnership, RestMethod.Delete, null, new RestParamList { new RestParam("id", OwnershipId) });                        
        }

        public Ownership GetOwnership(int OwnershipId)
        {

            return IntegrationService.RestCall<Ownership>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetOwnership, RestMethod.Get, null, new RestParamList { new RestParam("id", OwnershipId) });
            
        }

        public IList<Ownership> GetAll()
        {

            return IntegrationService.RestCall<List<Ownership>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllOwnerships, RestMethod.Get, null, null);

        }
    }
}
