using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.SpendItemsService;

namespace Administracion.Services.Implementations.SpendItems
{
    public class SpendItemService : ISpendItemsService
    {
        public ISync IntegrationService { get; set; }

        
        public IList<SpendItem> GetAll()
        {
            return IntegrationService.RestCall<List<SpendItem>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllSpendItems, RestMethod.Get, null, null);            
        }
        
    }
}
