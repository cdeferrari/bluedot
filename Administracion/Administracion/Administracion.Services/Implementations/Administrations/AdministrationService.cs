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

        public bool CreateAdministration(Administration administration)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateAdministration, RestMethod.Post, null, administration);                        
        }

        public bool UpdateAdministration(Administration administration)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateAdministration, administration.Id), RestMethod.Put, null, administration);                        
        }

        public bool DeleteAdministration(int administrationId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteAdministration, administrationId), RestMethod.Delete, null, new RestParamList { new RestParam("id", administrationId) });                        
        }

        public Administration GetAdministration(int administrationId)
        {

            return IntegrationService.RestCall<Administration>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetAdministration, administrationId), RestMethod.Get, null, new RestParamList { new RestParam("id", administrationId) });
            
        }

        public IList<Administration> GetAll()
        {

            return IntegrationService.RestCall<List<Administration>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllAdministrations, RestMethod.Get, null, null);

        }
    }
}
