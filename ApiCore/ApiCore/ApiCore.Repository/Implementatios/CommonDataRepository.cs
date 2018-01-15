using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Repository.Implementatios
{
    public class CommonDataRepository : Repository<CommonData>, ICommonDataRepository
    {
        public IList<CommonData> GetByOwnership(int id)
        {
            var result = this.Context.Set<CommonData>().Where(x => x.Ownership.Id == id)
                .ToList();
            return result;

        }
    }
}
