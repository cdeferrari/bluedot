using ApiCore.Services.Contracts.Messages;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Messages
{
    public class MessageService : IMessageService
    {
        public virtual IMessageRepository MessageRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public ITicketRepository TicketRepository { get; set; }


        [Transaction]
        public Message CreateMessage(MessageRequest Message)
        {
            var entityToInsert = new Message()
            {
                Sender = this.UserRepository.GetById(Message.SenderId),
                Content = Message.Content,
                Date = Message.Date,
                Ticket = this.TicketRepository.GetById(Message.TicketId)
            };

            if (Message.ReceiverId.HasValue)
            {
                entityToInsert.Receiver = this.UserRepository.GetById(Message.ReceiverId.Value);
            }

            MessageRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Message GetById(int MessageId)
        {
            var Message = MessageRepository.GetById(MessageId);
            if (Message == null)
                throw new BadRequestException(ErrorMessages.MensajeNoEncontrado);
            
            return Message;
        }
        

        //[Transaction]
        //public Message UpdateMessage(Message originalMessage, MessageRequest Message)
        //{            
        //    this.MergeMessage(originalMessage, Message);
        //    MessageRepository.Update(originalMessage);
        //    return originalMessage;
        //}
        

        [Transaction]
        public void DeleteMessage(int MessageId)
        {
            var Message = MessageRepository.GetById(MessageId);
            MessageRepository.Delete(Message);
        }
        

        //private void MergeMessage(Message originalMessage, MessageRequest Message)
        //{
        //    originalMessage. = this.UserRepository.GetById(Message.UserId);
        //    originalMessage.Consortium = this.ConsortiumRepository.GetById(Message.ConsortiumId);
        //    originalMessage.Home = Message.Home;
        //    originalMessage.IsAlternate = Message.IsAlternate;
        //    originalMessage.Male = Message.Male;
        //    originalMessage.LaborUnion = this.LaboralUnionRepository.GetById(Message.LaborUnionId);
        //    originalMessage.Salary = Message.Salary;
        //    originalMessage.StartDate = Message.StartDate;
        //    originalMessage.BirthDate = Message.BirthDate;
        //    originalMessage.WorkInsurance = Message.WorkInsurance;
        //    originalMessage.ExtraHourValue = Message.ExtraHourValue;
        //    originalMessage.PantsWaist = Message.PantsWaist;
        //    originalMessage.ShirtWaist = Message.ShirtWaist;
        //    originalMessage.FootwearWaist = Message.FootwearWaist;
        //    originalMessage.Schedule = Message.Schedule;
        //}

        [Transaction]
        public List<Message> GetAll()
        {
            return MessageRepository.GetAll().ToList();
            
        }

    }
}

