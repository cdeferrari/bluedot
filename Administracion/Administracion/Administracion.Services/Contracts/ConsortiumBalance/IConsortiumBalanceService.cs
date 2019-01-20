using Administracion.DomainModel;
using Administracion.Services.Implementations.ConsortiumBalance;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Administracion.Services.Contracts.ConsortiumBalance
{
    public interface IConsortiumBalanceService
    {
        ConsortiumBalanceStruct GetBalance(Consortium consortium, IList<UnitAccountStatusSummary> balances, int month);
        byte[] GetPDFBalance(ConsortiumBalanceStruct consortiumBalanceStruct);
    }
}
