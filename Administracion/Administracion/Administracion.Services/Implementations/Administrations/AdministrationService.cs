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
using Administracion.Services.Contracts.Administrations;

namespace Administracion.Services.Implementations.Administrations
{
    public class AdministrationService : IAdministrationService
    {
        public ISync IntegrationService { get; set; }

        public void CreateAdministration(Administration administration)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateAdministration, RestMethod.Post, null, new RestParamList { new RestParam("administration", administration) });                        
        }

        public void UpdateAdministration(Administration administration)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateAdministration, RestMethod.Put, null, new RestParamList {new RestParam("id", administration.Id), new RestParam("Administration", administration) });                        
        }

        public void DeleteAdministration(int administrationId)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteAdministration, RestMethod.Delete, null, new RestParamList { new RestParam("id", administrationId) });                        
        }

        public Administration GetAdministration(int administrationId)
        {

            return IntegrationService.RestCall<Administration>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAdministration, RestMethod.Get, null, new RestParamList { new RestParam("id", administrationId) });
            
        }

        public IList<Administration> GetAll()
        {

            return IntegrationService.RestCall<List<Administration>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllAdministrations, RestMethod.Get, null, null);

        }
    }
}
