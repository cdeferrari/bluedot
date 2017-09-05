using Administracion.Services.Contracts.Autentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Integration.Model;

namespace Administracion.Services.Implementations.Autentication
{
    public class Authentication : IAuthentication
    {
        public ISync IntegrationService { get; set; }

        public Account Login(string userName, string password)
        {
            //var result = IntegrationService.RestCall<Account>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.Login, RestMethod.Post, null, new RestParamList() {new RestParam("userName", userName), new RestParam("password", password) } );
            var result = new Account()
            {
                Email = userName,
                Id = 1,
                Password = password,
                Roles = DomainModel.Enum.Roles.Client,
                UserName = userName
            }; 
            return result;
            
        }

        public void SaveAccessToken(string AccessToken)
        {
            throw new NotImplementedException();
        }
    }
}
