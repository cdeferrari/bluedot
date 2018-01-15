using Administracion.DomainModel;
using Administracion.Dto.Consortium;
using Administracion.Dto.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Provinces
{
    public interface IProvinceService
    {
        IList<Province> GetAllProvinces();
        bool CreateProvince(ProvinceRequest laboralUnion);

        bool DeleteProvince(int id);
    }
}
