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
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateUser, RestMethod.Put, null, new RestParamList { new RestParam("id", User.Id) , new RestParam("User", JsonConvert.SerializeObject(User)) });                        
        }

        public bool DeleteUser(int usertId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteUser, RestMethod.Delete, null, new RestParamList { new RestParam("id", userId) });                        
        }

        public User GetUser(int userId)
        {
            return IntegrationService.RestCall<User>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetUser, RestMethod.Delete, null, new RestParamList { new RestParam("id", userId) });                        
            
        }

        public IList<Ticket> GetAll()
        {
            return IntegrationService.RestCall<IList<Ticket>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetUser, RestMethod.Delete, null, null);                                    
        }
      
    }
}
