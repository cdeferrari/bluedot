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

        public bool CreateTicket(Ticket ticket)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateTicket, RestMethod.Post, null, ticket);                        
        }

        public bool UpdateTicket(Ticket ticket)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateTicket, RestMethod.Put, null, new RestParamList { new RestParam("id", ticket.Id) , new RestParam("ticket", JsonConvert.SerializeObject(ticket)) });                        
        }

        public bool DeleteTicket(int ticketId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteTicket, RestMethod.Delete, null, new RestParamList { new RestParam("id", ticketId) });                        
        }

        Ticket ITicketService.GetTicket(int ticketId)
        {
            throw new NotImplementedException();
        }
      
    }
}
