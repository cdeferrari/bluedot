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
using Administracion.Services.Contracts.Owners;
using Administracion.Dto.Owner;

namespace Administracion.Services.Implementations.Owners
{
    public class OwnerService : IOwnerService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateOwner(OwnerRequest Owner)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateOwner, RestMethod.Post, null, Owner);                        
        }

        public bool UpdateOwner(Owner Owner)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateOwner, Owner.Id), RestMethod.Put, null, Owner);                        
        }

        public bool DeleteOwner(int OwnerId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format( ApiCore.DeleteOwner, OwnerId), RestMethod.Delete, null, new RestParamList { new RestParam("id", OwnerId) });                        
        }

        public Owner GetOwner(int OwnerId)
        {
            return IntegrationService.RestCall<Owner>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetOwner, RestMethod.Delete, null, new RestParamList { new RestParam("id", OwnerId) });                        
            
        }

        public IList<Owner> GetAll()
        {
            return IntegrationService.RestCall<List<Owner>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllOwners, RestMethod.Get, null, null);                                    
        }
      
    }
}
