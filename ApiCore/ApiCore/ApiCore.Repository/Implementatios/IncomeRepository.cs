using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class IncomeRepository : Repository<Income>, IIncomeRepository
    {
        public IList<Income> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            var result = this.Context.Set<Income>().Where(x => x.Consortium.Id == consortiumId
            && x.IncomeDate <= endDate && x.IncomeDate >= startDate)
                .ToList();
            return result;
        }

    }
}
