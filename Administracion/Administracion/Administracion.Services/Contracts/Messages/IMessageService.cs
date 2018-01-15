using Administracion.DomainModel;
using Administracion.Dto.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Messages
{
    public interface IMessageService
    {
        IList<Message> GetAll();
        Message GetMessage(int MessageId);
        Entidad CreateMessage(MessageRequest Message);
        //bool UpdateMessage(MessageRequest Message);
        bool DeleteMessage(int MessageId);
    }
}
