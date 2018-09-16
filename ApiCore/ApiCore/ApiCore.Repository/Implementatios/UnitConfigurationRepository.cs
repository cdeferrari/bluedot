using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class UnitConfigurationRepository : Repository<UnitConfiguration>, IUnitConfigurationRepository
    {
        public IList<UnitConfiguration> GetByUnitId(int UnitId, DateTime startDate, DateTime endDate)
        {
            var result = this.Context.Set<UnitConfiguration>().Where(x => x.Unit.Id == UnitId
            && x.ConfigurationDate <= endDate && x.ConfigurationDate >= startDate)
                .ToList();
            return result;
        }

    }
}
