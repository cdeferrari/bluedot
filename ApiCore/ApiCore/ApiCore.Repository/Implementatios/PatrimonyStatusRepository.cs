using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class PatrimonyStatusRepository : Repository<PatrimonyStatus>, IPatrimonyStatusRepository
    {
        public IList<PatrimonyStatus> GetByConsortiumId(int consortiumId)
        {
            var result = this.Context.Set<PatrimonyStatus>().Where(x => x.Consortium.Id == consortiumId)
                .ToList();
            return result;
        }

    }
}
