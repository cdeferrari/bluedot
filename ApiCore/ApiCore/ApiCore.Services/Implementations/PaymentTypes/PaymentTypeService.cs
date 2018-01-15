using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.TaskResult;
using ApiCore.Services.Contracts.PaymentTypes;
using ApiCore.Dtos;
using System.Linq;

namespace ApiCore.Services.Implementations.TaskResult
{

    public class PaymentTypeService : IPaymentTypeService
    {
        public IPaymentTypeRepository PaymentTypeRepository { get; set; }


        [Transaction]
        public PaymentType CreatePaymentType(DescriptionRequest paymentType)
        {
            var entity = new PaymentType() { Description = paymentType.Description };
            this.PaymentTypeRepository.Insert(entity);
            return entity;
        }
        [Transaction]
        public void Delete(int paymentTypeId)
        {
            var entity = this.PaymentTypeRepository.GetById(paymentTypeId);
            this.PaymentTypeRepository.Delete(entity);
        }

        [Transaction]
        public IList<DomainModel.PaymentType> GetAll()
        {
            return PaymentTypeRepository.GetAll().ToList();
            
        }
        

    }
}
