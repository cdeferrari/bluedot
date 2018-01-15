using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Messages
{
    public interface IMessageService
    {
        Message GetById(int messageId);
        //Message Update(Message message);
        Message CreateMessage(MessageRequest request);
        void DeleteMessage(int messageId);
        //Message UpdateMessage(Message originalMessage, MessageRequest message);
        List<Message> GetAll();

    }
}
