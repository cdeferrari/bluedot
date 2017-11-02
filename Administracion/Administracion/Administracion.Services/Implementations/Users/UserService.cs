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

        public User CreateUser(User user)
        {
          return  IntegrationService.RestCall<User>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateUser, RestMethod.Post, null, user);                        
        }

        public bool UpdateUser(User user)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateUser, user.Id), RestMethod.Put, null, user);                        
        }

        public bool DeleteUser(int userId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteUser, userId), RestMethod.Delete, null, new RestParamList { new RestParam("id", userId) });                        
        }

        public User GetUser(int userId)
        {
            return IntegrationService.RestCall<User>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetUser, userId) , RestMethod.Get, null, new RestParamList { new RestParam("id", userId) });                        
            
        }

        public IList<User> GetAll()
        {
            return IntegrationService.RestCall<List<User>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllUsers, RestMethod.Get, null, null);                                    
        }
      
    }
}
