using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Providers
{
    public interface IProviderService
    {
        Provider CreateProvider(ProviderRequest Provider);
        Provider GetById(int ProviderId);        
        Provider UpdateProvider(Provider originalProvider, ProviderRequest Provider);
        void DeleteProvider(int ProviderId);
        List<Provider> GetAll();
    }
}
