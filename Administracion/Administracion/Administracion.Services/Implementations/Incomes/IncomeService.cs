using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.Incomes;
using Administracion.Dto.Income;
using System;

namespace Administracion.Services.Implementations.Incomes
{
    public class IncomeService : IIncomeService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateIncome(IncomeRequest Income)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateIncome, RestMethod.Post, null, Income);                        
        }

        public bool UpdateIncome(IncomeRequest Income)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateIncome, Income.Id), RestMethod.Put, null, Income);
        }

        public bool DeleteIncome(int IncomeId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteIncome, IncomeId), RestMethod.Delete, null, new RestParamList { new RestParam("id", IncomeId) });                        
        }

        public Income GetIncome(int IncomeId)
        {

            return IntegrationService.RestCall<Income>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetIncome, IncomeId), RestMethod.Get, null, new RestParamList { new RestParam("id", IncomeId) });
            
        }

        public List<Income> GetAll()
        {
            return IntegrationService.RestCall<List<Income>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllIncomes, RestMethod.Get, null, null);            
        }

        public IList<Income> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            return IntegrationService.RestCall<List<Income>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetIncomeByConsortiumId, consortiumId, startDate, endDate), RestMethod.Get, null, null);
        }
    }
}
