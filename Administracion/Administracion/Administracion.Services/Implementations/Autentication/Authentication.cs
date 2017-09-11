using Administracion.Services.Contracts.Autentication;
using System;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Integration.Model;
using Administracion.Dto.Account;

namespace Administracion.Services.Implementations.Autentication
{
    public class Authentication : IAuthentication
    {
        public ISync IntegrationService { get; set; }

        public Account Login(string userName, string password)
        {
            var account = IntegrationService.RestCall<AccountResponse>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.Login, RestMethod.Post, new RestParamList() {new RestParam("email", userName, RestParamType.QueryString), new RestParam("password", password, RestParamType.QueryString) } );

            var result = new Account()
            {
                Email = account.Email,
                Id = account.Id,
                Password = account.Password,
                Role = account.Role.Id == 1 ? DomainModel.Enum.Roles.Root : DomainModel.Enum.Roles.Client,
                Name = account.User.Name + " " + account.User.Surname
            }; 

            return result;
            
        }

        public void SaveAccessToken(string AccessToken)
        {
            throw new NotImplementedException();
        }
    }
}
