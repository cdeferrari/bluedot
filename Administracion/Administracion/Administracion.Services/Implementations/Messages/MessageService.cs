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
using Administracion.Services.Contracts.Messages;
using Administracion.Dto.Message;

namespace Administracion.Services.Implementations.Messages
{
    public class MessageService : IMessageService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateMessage(MessageRequest Message)
        {
          return IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateMessage, RestMethod.Post, null, Message);                        
        }

        //public bool UpdateMessage(MessageRequest Message)
        //{
        //    return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.UpdateMessage, Message.Id), RestMethod.Put, null, Message);                        
        //}

        public bool DeleteMessage(int MessageId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format( ApiCore.DeleteMessage, MessageId), RestMethod.Delete, null, new RestParamList { new RestParam("id", MessageId) });                        
        }

        public Message GetMessage(int MessageId)
        {
            return IntegrationService.RestCall<Message>(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.GetMessage, MessageId), RestMethod.Get, null, new RestParamList { new RestParam("id", MessageId) });                        
            
        }

        public IList<Message> GetAll()
        {
            return IntegrationService.RestCall<List<Message>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllMessages, RestMethod.Get, null, null);                                    
        }
      
    }
}
