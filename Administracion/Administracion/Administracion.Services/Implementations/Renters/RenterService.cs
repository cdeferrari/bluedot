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
using Administracion.Services.Contracts.Renters;
using Administracion.Dto.Renter;

namespace Administracion.Services.Implementations.Renters
{
    public class RenterService : IRenterService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateRenter(RenterRequest Renter)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateRenter, RestMethod.Post, null, Renter);                        
        }

        public bool UpdateRenter(Renter Renter)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateRenter, Renter.Id), RestMethod.Put, null, Renter);                        
        }

        public bool DeleteRenter(int RenterId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteRenter, RenterId), RestMethod.Delete, null, new RestParamList { new RestParam("id", RenterId) });                        
        }

        public Renter GetRenter(int RenterId)
        {
            return IntegrationService.RestCall<Renter>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetRenter, RenterId) , RestMethod.Get, null, new RestParamList { new RestParam("id", RenterId) });                        
            
        }

        public IList<Renter> GetAll()
        {
            return IntegrationService.RestCall<List<Renter>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllRenter, RestMethod.Get, null, null);                                    
        }
      
    }
}
