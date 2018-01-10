using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.Messages;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administracion.Services.Contracts.LaboralUnion;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Dto.Message;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.All)]
    public class MessagesController : Controller
    {
        public virtual IMessageService Messageservice { get; set; }
        public virtual IUserService UserService { get; set; }
        
        [HttpPost]
        public ActionResult CreateMessage(MessageViewModel Message)
        {
         
            var nMessages = Mapper.Map<MessageRequest>(Message);                        

            try
            {
                nMessages.Date = DateTime.Now;                
                nMessages.SenderId = SessionPersister.Account.User.Id;
                var entity = this.Messageservice.CreateMessage(nMessages);
                var result = entity.Id != 0;                
                if (result)
                {
                    return Redirect(string.Format("/Backlog/UpdateTicketById/{0}", Message.TicketId));
                }
                else
                {
                    return View("../Shared/Error");
                }
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }


        
        public ActionResult DeleteMessages(int id, int ticketId)
        {                    
            this.Messageservice.DeleteMessage(id);

            return Redirect(string.Format("/Backlog/UpdateTicketById/{0}", ticketId));
        }
        
        
    }
}
