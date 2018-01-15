using Administracion.DomainModel;
using Administracion.Dto.Consortium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Countries
{
    public interface ICountryService
    {
        IList<Province> GetAllProvinces(int countryId);
        
    }
}
