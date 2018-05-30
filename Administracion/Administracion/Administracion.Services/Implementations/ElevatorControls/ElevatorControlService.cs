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
using Administracion.Services.Contracts.ElevatorControlService;

namespace Administracion.Services.Implementations.ElevatorControls
{
    public class ElevatorControlService : IElevatorControlService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateElevatorControl(ControlRequest ElevatorControl)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateElevatorControl, RestMethod.Post, null, ElevatorControl);                        
        }

        public bool UpdateElevatorControl(ControlRequest ElevatorControl)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateElevatorControl, ElevatorControl.Id), RestMethod.Put, null, ElevatorControl);                        
        }

        public bool DeleteElevatorControl(int ElevatorControlId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format( ApiCore.DeleteElevatorControl, ElevatorControlId), RestMethod.Delete, null, new RestParamList { new RestParam("id", ElevatorControlId) });                        
        }

        public ElevatorControl GetElevatorControl(int ElevatorControlId)
        {
            return IntegrationService.RestCall<ElevatorControl>(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.GetElevatorControl, ElevatorControlId), RestMethod.Get, null, new RestParamList { new RestParam("id", ElevatorControlId) });                        
            
        }

        public IList<ElevatorControl> GetAll()
        {
            return IntegrationService.RestCall<List<ElevatorControl>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllElevatorControls, RestMethod.Get, null, null);                                    
        }
      
    }
}
