using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        public IList<Province> GetByCountryId(int id)
        {
            var result = this.Context.Set<Province>().Where(x => x.Country.Id == id)
                .ToList();
            return result;
        }
    }
}
