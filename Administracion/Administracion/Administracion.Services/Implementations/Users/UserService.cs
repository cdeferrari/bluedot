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
using Administracion.Services.Contracts.Users;

namespace Administracion.Services.Implementations.Users
{
    public class UserService : IUserService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateUser(User user)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateUser, RestMethod.Post, null, user);                        
        }

        public bool UpdateUser(User user)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateUser, RestMethod.Put, null, new RestParamList { new RestParam("id", user.Id) , new RestParam("User", user) });                        
        }

        public bool DeleteUser(int userId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteUser, RestMethod.Delete, null, new RestParamList { new RestParam("id", userId) });                        
        }

        public User GetUser(int userId)
        {
            return IntegrationService.RestCall<User>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetUser, RestMethod.Delete, null, new RestParamList { new RestParam("id", userId) });                        
            
        }

        public IList<User> GetAll()
        {
            return IntegrationService.RestCall<List<User>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetUser, RestMethod.Get, null, null);                                    
        }
      
    }
}
