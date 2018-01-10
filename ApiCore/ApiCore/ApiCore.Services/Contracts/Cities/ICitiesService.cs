using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Cities
{
    public interface ICitiesService
    {
        IList<City> GetAll();        
        City CreateCity(CityRequest city);
        void Delete(int cityId);

    }
}
