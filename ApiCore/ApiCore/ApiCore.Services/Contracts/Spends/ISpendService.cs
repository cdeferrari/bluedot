using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Spends
{
    public interface ISpendService
    {
        Spend CreateSpend(SpendRequest Spend);
        Spend GetById(int SpendId);
        IList<Spend> GetAll();
        IList<Spend> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate);
        Spend UpdateSpend(Spend originalSpend, SpendRequest Spend);
        void DeleteSpend(int SpendId);        
    }
}
