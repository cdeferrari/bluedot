using Administracion.DomainModel;
using Administracion.Dto.Spend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Spends
{
    public interface ISpendService
    {
        List<Spend> GetAll();
        Spend GetSpend(int SpendId);
        bool CreateSpend(SpendRequest Spend);
        bool UpdateSpend(SpendRequest Spend);
        bool DeleteSpend(int SpendId);        
        IList<Spend> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate);
    }
}
