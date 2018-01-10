using Administracion.Services.Contracts.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.Countries;

namespace Administracion.Services.Implementations.Countrys
{
    public class CountryService : ICountryService
    {
        public ISync IntegrationService { get; set; }

        

        public IList<DomainModel.Province> GetAllProvinces(int countryId)
        {
            return IntegrationService.RestCall<List<DomainModel.Province>>(ConfigurationManager.AppSettings["ApiCoreUrl"],string.Format(ApiCore.GetProvincesByCountry, countryId), RestMethod.Get, null, new RestParamList { new RestParam("id", countryId) });
            
        }
    }
}
