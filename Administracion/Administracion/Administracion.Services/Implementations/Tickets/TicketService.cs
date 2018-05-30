using Administracion.Services.Contracts.Tickets;
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Dto.Ticket;

namespace Administracion.Services.Implementations.Tickets
{
    public class TicketService : ITicketService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateTicket(TicketRequest ticket)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateTicket, RestMethod.Post, null, ticket);                        
        }

        public bool UpdateTicket(TicketRequest ticket)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateTicket, ticket.Id), RestMethod.Put,  null, ticket);                        
        }

        public bool DeleteTicket(int ticketId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteTicket, ticketId), RestMethod.Delete, null, new RestParamList { new RestParam("id", ticketId) });                        
        }

        public Ticket GetTicket(int ticketId)
        {
            return IntegrationService.RestCall<Ticket>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetTicket, ticketId.ToString()), RestMethod.Get, null, null);                        
            
        }

        public IList<Ticket> GetAll()
        {
            return IntegrationService.RestCall<List<Ticket>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllTicket, RestMethod.Get, null, null);                                    
        }

        public IList<Ticket> GetByConsortiumId(int consortiumId)
        {
            return IntegrationService.RestCall<List<Ticket>>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetByConsortiumId, consortiumId), RestMethod.Get, null, null);
        }

        public bool CreateTicketHistory(TicketHistoryRequest Ticket)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateTicketHistory, RestMethod.Post, null, Ticket);
        }
    }
}
