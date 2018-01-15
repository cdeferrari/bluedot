using Administracion.DomainModel;
using Administracion.Dto.City;
using Administracion.Dto.Consortium;
using Administracion.Dto.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Provinces
{
    public interface ICityService
    {
        IList<City> GetAll();
        bool CreateCity(CityRequest city);

        bool DeleteCity(int id);
    }
}
