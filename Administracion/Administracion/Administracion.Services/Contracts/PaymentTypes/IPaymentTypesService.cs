using Administracion.DomainModel;
using Administracion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.PaymentTypesService
{
    public interface IPaymentTypesService
    {
        IList<PaymentType> GetAll();
        bool CreatePaymentType(DescriptionRequest paymentType);
        bool DeletePaymentType(int id);
    }
}
