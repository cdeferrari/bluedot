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

        public bool CreateConsortium(ConsortiumRequest consortium)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateConsortium, RestMethod.Post, null, consortium);                        
        }

        public bool UpdateConsortium(ConsortiumRequest consortium)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateConsortium, consortium.Id), RestMethod.Put, null, consortium);
        }

        public bool DeleteConsortium(int consortiumId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteConsortium, consortiumId), RestMethod.Delete, null, new RestParamList { new RestParam("id", consortiumId) });                        
        }

        public Consortium GetConsortium(int consortiumId)
        {

            return IntegrationService.RestCall<Consortium>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetConsortium, consortiumId), RestMethod.Get, null, new RestParamList { new RestParam("id", consortiumId) });
            
        }

        public List<Consortium> GetAll()
        {
            return IntegrationService.RestCall<List<Consortium>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllConsortium, RestMethod.Get, null, null);
            
        }

        public IList<List> GetAllChecklists(int consortiumId)
        {
            return IntegrationService.RestCall<List<List>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetChecklistsByConsortium, RestMethod.Get, null, new RestParamList { new RestParam("id", consortiumId) });
            
        }

        public bool CloseMonth(int consortiumId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.CloseMonth, consortiumId), RestMethod.Post, null, new RestParamList { new RestParam("id", consortiumId) });
        }

        public bool RegisterUnitsMonthDebt(int consortiumId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.RegisterUnitsMonthDebt, consortiumId), RestMethod.Post, null, new RestParamList { new RestParam("id", consortiumId) });
        }

    }
}
