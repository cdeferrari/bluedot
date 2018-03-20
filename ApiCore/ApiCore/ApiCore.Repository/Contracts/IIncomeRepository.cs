using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Contracts
{
    public interface IIncomeRepository : IRepository<Income>
    {
        IList<Income> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate);
    }
}
