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
using Administracion.Dto.Control;
using Administracion.Services.Contracts.FireExtinguisherControlService;

namespace Administracion.Services.Implementations.FireExtinguisherControls
{
    public class FireExtinguisherControlService : IFireExtinguisherControlService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateFireExtinguisherControl(ControlRequest FireExtinguisherControl)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateFireExtinguisherControl, RestMethod.Post, null, FireExtinguisherControl);                        
        }

        public bool UpdateFireExtinguisherControl(ControlRequest FireExtinguisherControl)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateFireExtinguisherControl, FireExtinguisherControl.Id), RestMethod.Put, null, FireExtinguisherControl);                        
        }

        public bool DeleteFireExtinguisherControl(int FireExtinguisherControlId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format( ApiCore.DeleteFireExtinguisherControl, FireExtinguisherControlId), RestMethod.Delete, null, new RestParamList { new RestParam("id", FireExtinguisherControlId) });                        
        }

        public FireExtinguisherControl GetFireExtinguisherControl(int FireExtinguisherControlId)
        {
            return IntegrationService.RestCall<FireExtinguisherControl>(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.GetFireExtinguisherControl, FireExtinguisherControlId), RestMethod.Get, null, new RestParamList { new RestParam("id", FireExtinguisherControlId) });                        
            
        }

        public IList<FireExtinguisherControl> GetAll()
        {
            return IntegrationService.RestCall<List<FireExtinguisherControl>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllFireExtinguisherControls, RestMethod.Get, null, null);                                    
        }
      
    }
}
