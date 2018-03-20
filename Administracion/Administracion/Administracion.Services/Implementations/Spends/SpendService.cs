using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.Spends;
using Administracion.Dto.Spend;
using System;

namespace Administracion.Services.Implementations.Spends
{
    public class SpendService : ISpendService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateSpend(SpendRequest Spend)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateSpend, RestMethod.Post, null, Spend);                        
        }

        public bool UpdateSpend(SpendRequest Spend)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateSpend, Spend.Id), RestMethod.Put, null, Spend);
        }
        

        public bool DeleteSpend(int SpendId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteSpend, SpendId), RestMethod.Delete, null, new RestParamList { new RestParam("id", SpendId) });                        
        }

        public Spend GetSpend(int SpendId)
        {

            return IntegrationService.RestCall<Spend>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetSpend, SpendId), RestMethod.Get, null, new RestParamList { new RestParam("id", SpendId) });
            
        }

        public List<Spend> GetAll()
        {
            return IntegrationService.RestCall<List<Spend>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllSpends, RestMethod.Get, null, null);            
        }

        public IList<Spend> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            var sstartDate = startDate.Date.ToShortDateString();
            var sendDate = endDate.Date.ToShortDateString();
            return IntegrationService.RestCall<List<Spend>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetSpendByConsortium, consortiumId,sstartDate,sendDate), RestMethod.Get, null, null);
        }
    }
}
