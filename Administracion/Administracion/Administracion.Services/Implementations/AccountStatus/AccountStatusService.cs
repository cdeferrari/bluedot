using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.AccountStatuss;
using System;
using Administracion.Dto.Account;

namespace Administracion.Services.Implementations.AccountStatuss
{
    public class AccountStatusService : IAccountStatusService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateAccountStatus(AccountStatusRequest AccountStatus)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateAccountStatus, RestMethod.Post, null, AccountStatus);                        
        }

        public bool UpdateAccountStatus(AccountStatusRequest AccountStatus)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateAccountStatus, RestMethod.Post, null, AccountStatus);
            //return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateAccountStatus, AccountStatus.Id), RestMethod.Put, null, AccountStatus);
        }

        public bool DeleteAccountStatus(int AccountStatusId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteAccountStatus, AccountStatusId), RestMethod.Delete, null, new RestParamList { new RestParam("id", AccountStatusId) });
        }

        public AccountStatus GetAccountStatus(int AccountStatusId)
        {

            return IntegrationService.RestCall<AccountStatus>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetAccountStatus, AccountStatusId), RestMethod.Get, null, new RestParamList { new RestParam("id", AccountStatusId) });

        }

        public List<AccountStatus> GetAll()
        {
            return IntegrationService.RestCall<List<AccountStatus>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllAccountStatus, RestMethod.Get, null, null);
        }

        //public IList<AccountStatus> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        //{
        //    return IntegrationService.RestCall<List<AccountStatus>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetAccountStatusByConsortiumId, consortiumId, startDate, endDate), RestMethod.Get, null, null);
        //}
    }
}
