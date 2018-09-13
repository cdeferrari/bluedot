using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Contracts
{
    public interface IAccountStatusRepository : IRepository<AccountStatus>
    {
        IList<AccountStatus> GetByUnitId(int unitId);
    }
}
