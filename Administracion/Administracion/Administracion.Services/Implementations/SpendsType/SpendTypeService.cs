using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.SpendTypes;
using Administracion.Dto.SpendType;

namespace Administracion.Services.Implementations.SpendTypes
{
    public class SpendTypeService : ISpendTypeService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateSpendType(SpendTypeRequest SpendType)
        {
            return IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateSpendType, RestMethod.Post, null, SpendType);                        
        }

        public bool UpdateSpendType(SpendTypeRequest SpendType)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateSpendType, SpendType.Id), RestMethod.Put, null, SpendType);
        }

        public bool DeleteSpendType(int SpendTypeId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteSpendType, SpendTypeId), RestMethod.Delete, null, new RestParamList { new RestParam("id", SpendTypeId) });                        
        }

        public SpendType GetSpendType(int SpendTypeId)
        {

            return IntegrationService.RestCall<SpendType>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetSpendType, SpendTypeId), RestMethod.Get, null, new RestParamList { new RestParam("id", SpendTypeId) });
            
        }

        public List<SpendType> GetAll()
        {
            return IntegrationService.RestCall<List<SpendType>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllSpendType, RestMethod.Get, null, null);            
        }
        
    }
}
