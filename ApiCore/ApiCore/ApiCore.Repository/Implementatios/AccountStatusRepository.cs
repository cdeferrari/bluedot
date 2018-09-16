using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class AccountStatusRepository : Repository<AccountStatus>, IAccountStatusRepository
    {
        public IList<AccountStatus> GetByUnitId(int unitId)
        {
            var result = this.Context.Set<AccountStatus>().Where(x => x.Unit.Id == unitId)
                .ToList();
            return result;
        }

    }
}
