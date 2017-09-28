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
using Administracion.Services.Contracts.Consortiums;
using Administracion.Dto.Consortium;

namespace Administracion.Services.Implementations.Consortiums
{
    public class ConsortiumService : IConsortiumService
    {
        public ISync IntegrationService { get; set; }

        public void CreateConsortium(ConsortiumRequest consortium)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateConsortium, RestMethod.Post, null, consortium);                        
        }

        public void UpdateConsortium(Consortium consortium)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateConsortium, RestMethod.Put, null, new RestParamList {new RestParam("id", consortium.Id), new RestParam("consortium", consortium) });                        
        }

        public void DeleteConsortium(int consortiumId)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteConsortium, RestMethod.Delete, null, new RestParamList { new RestParam("id", consortiumId) });                        
        }

        public Consortium GetConsortium(int consortiumId)
        {

            return IntegrationService.RestCall<Consortium>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetConsortium, RestMethod.Get, null, new RestParamList { new RestParam("id", consortiumId) });
            
        }

        public List<Consortium> GetAll()
        {
            return IntegrationService.RestCall<List<Consortium>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetConsortium, RestMethod.Get, null, null);
            
        }
    }
}
