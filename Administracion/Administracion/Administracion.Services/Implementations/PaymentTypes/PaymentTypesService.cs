
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.Status;
using Administracion.Services.Contracts.PaymentTypesService;
using Administracion.Dto;

namespace Administracion.Services.Implementations.Status
{
    public class PaymentTypesService : IPaymentTypesService
    {
        public ISync IntegrationService { get; set; }

        public bool CreatePaymentType(DescriptionRequest paymentType)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreatePaymentType, RestMethod.Post, null, paymentType);
            
        }

        public bool DeletePaymentType(int id)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeletePaymentType, id), RestMethod.Delete, null, new RestParamList { new RestParam("id", id) });
        }

        public IList<PaymentType> GetAll()
        {
            return IntegrationService.RestCall<List<PaymentType>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllPaymentTypes, RestMethod.Get, null, null);                                    
        }
      
    }
}
