using Administracion.DomainModel;
using Administracion.Dto.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Providers
{
    public interface IProviderService
    {
        IList<Provider> GetAll();
        Provider GetProvider(int ProviderId);
        bool CreateProvider(ProviderRequest Provider);
        bool UpdateProvider(ProviderRequest Provider);
        bool DeleteProvider(int ProviderId);
    }
}
