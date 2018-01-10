using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Contracts
{
    public interface ICommonDataRepository : IRepository<CommonData>
    {
        IList<CommonData> GetByOwnership(int id);
    }
}
