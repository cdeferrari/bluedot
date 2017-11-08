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
using Administracion.Services.Contracts.Managers;
using Administracion.Dto.Manager;

namespace Administracion.Services.Implementations.Managers
{
    public class ManagerService : IManagerService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateManager(ManagerRequest Manager)
        {
          return IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateManager, RestMethod.Post, null, Manager);                        
        }

        public bool UpdateManager(ManagerRequest Manager)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateManager, Manager.Id), RestMethod.Put, null, Manager);                        
        }

        public bool DeleteManager(int ManagerId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format( ApiCore.DeleteManager, ManagerId), RestMethod.Delete, null, new RestParamList { new RestParam("id", ManagerId) });                        
        }

        public Manager GetManager(int ManagerId)
        {
            return IntegrationService.RestCall<Manager>(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.GetManager, ManagerId), RestMethod.Get, null, new RestParamList { new RestParam("id", ManagerId) });                        
            
        }

        public IList<Manager> GetAll()
        {
            return IntegrationService.RestCall<List<Manager>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllManagers, RestMethod.Get, null, null);                                    
        }
      
    }
}
