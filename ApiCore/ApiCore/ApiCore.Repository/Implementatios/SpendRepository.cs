using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class SpendRepository : Repository<Spend>, ISpendRepository
    {
        public IList<Spend> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            var result = this.Context.Set<Spend>().Where(x => x.Consortium.Id == consortiumId  
            && x.PaymentDate <= endDate && x.PaymentDate >= startDate)
                .ToList();
            return result;
        }

    }
}
