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
using Administracion.Services.Contracts.Managers;
using Administracion.Dto.Manager;

namespace Administracion.Services.Implementations.Managers
{
    public class AccountService : IAccountService
    {
        public ISync IntegrationService { get; set; }

        public IList<BacklogUser> GetAll()
        {
            return IntegrationService.RestCall<List<BacklogUser>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllBacklogUsers, RestMethod.Get, null, null);                                    
        }
      
    }
}
