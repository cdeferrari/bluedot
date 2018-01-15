using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Provinces
{
    public interface IProvincesService
    {
        IList<Province> GetAll();
        IList<Province> GetByCountryId(int id);
        Province CreateProvince(ProvinceRequest province);
        void Delete(int provinceId);

    }
}
