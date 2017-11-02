using Administracion.Services.Contracts.Autentication;
using System;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Integration.Model;
using Administracion.Dto.Account;
using System.Collections.Generic;

namespace Administracion.Services.Implementations.Autentication
{
    public class Authentication : IAuthentication
    {
        public ISync IntegrationService { get; set; }

        public List<Account> GetAll()
        {
            var accounts = IntegrationService.RestCall<List<AccountResponse>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllAccounts, RestMethod.Get,null, null);
            var result = new List<Account>();

            accounts.ForEach(x => result.Add(this.MapAccount(x)));
            return result;
        }

        public Account Login(string userName, string password)
        {
            try
            {
                var account = IntegrationService.RestCall<AccountResponse>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.Login, RestMethod.Post, new RestParamList() { new RestParam("email", userName, RestParamType.QueryString), new RestParam("password", password, RestParamType.QueryString) });

                var result = this.MapAccount(account);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            

        }

        public void SaveAccessToken(string AccessToken)
        {
            throw new NotImplementedException();
        }

        private Account MapAccount(AccountResponse response)
        {
            var result = new Account()
            {
                Email = response.Email,
                Id = response.Id,
                Password = response.Password,
                Role = response.Role.Id == 1 ? DomainModel.Enum.Roles.Root : DomainModel.Enum.Roles.Client,
                Name = response.User.Name + " " + response.User.Surname
            };

            return result;
        }
    }
}
