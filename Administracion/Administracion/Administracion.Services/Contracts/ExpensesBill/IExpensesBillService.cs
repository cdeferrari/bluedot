using Administracion.DomainModel;
using Administracion.Services.Implementations.ExpensesBill;
using Administracion.Services.Implementations.PaymentTickets;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Administracion.Services.Contracts.ExpensesBill
{
    public interface IExpensesBillervice
    {
        ExpensesBillStruct GetExpensesBill(Consortium consortium, IList<Spend> paymentTickets, int month);
        byte[] GetPDFTickets(ExpensesBillStruct paymentTicketsStruct);
    }
}
