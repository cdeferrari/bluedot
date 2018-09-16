using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class ConsortiumConfigurationRepository : Repository<ConsortiumConfiguration>, IConsortiumConfigurationRepository
    {
        public IList<ConsortiumConfiguration> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            var result = this.Context.Set<ConsortiumConfiguration>().Where(x => x.Consortium.Id == consortiumId
            && x.ConfigurationDate <= endDate && x.ConfigurationDate >= startDate)
                .ToList();
            return result;
        }

    }
}
