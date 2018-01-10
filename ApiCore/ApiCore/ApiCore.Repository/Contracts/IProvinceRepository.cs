using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Contracts
{
    public interface IProvinceRepository : IRepository<Province>
    {
        IList<Province> GetByCountryId(int id);
    }
}
