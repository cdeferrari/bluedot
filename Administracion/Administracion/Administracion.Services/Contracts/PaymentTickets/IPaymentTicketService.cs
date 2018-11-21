using Administracion.DomainModel;
using Administracion.Services.Implementations.PaymentTickets;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Administracion.Services.Contracts.Consortiums
{
    public interface IPaymentTicketService
    {
        PaymentTicketsStruct GetTickets(Consortium consortium, IList<PaymentTicket> paymentTickets, int month);
        byte[] GetPDFTickets(PaymentTicketsStruct paymentTicketsStruct);
    }
}
