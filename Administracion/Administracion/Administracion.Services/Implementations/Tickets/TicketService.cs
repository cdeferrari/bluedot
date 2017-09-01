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

namespace Administracion.Services.Implementations.Tickets
{
    public class TicketService : ITicketService
    {
        public ISync IntegrationService { get; set; }

        public void CreateTicket(Ticket ticket)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateTicket, RestMethod.Post, null, new RestParamList { new RestParam("ticket", JsonConvert.SerializeObject(ticket)) });            
            
        }
    }
}
