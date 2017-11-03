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
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Dto.Unit;

namespace Administracion.Services.Implementations.FunctionalUnits
{
    public class FunctionalUnitService : IFunctionalUnitService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateFunctionalUnit(FunctionalUnitRequest FunctionalUnit)
        {
            return IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateFunctionalUnit, RestMethod.Post, null, FunctionalUnit);                        
        }

        public bool UpdateFunctionalUnit(FunctionalUnitRequest FunctionalUnit)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateFunctionalUnit, FunctionalUnit.Id), RestMethod.Put, null, FunctionalUnit);                        
        }

        public bool DeleteFunctionalUnit(int FunctionalUnitId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteFunctionalUnit, FunctionalUnitId), RestMethod.Delete, null, new RestParamList { new RestParam("id", FunctionalUnitId) });                        
        }

        public FunctionalUnit GetFunctionalUnit(int FunctionalUnitId)
        {

            return IntegrationService.RestCall<FunctionalUnit>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetFunctionalUnit, FunctionalUnitId), RestMethod.Get, null, new RestParamList { new RestParam("id", FunctionalUnitId) });
            
        }

        public IList<FunctionalUnit> GetAll()
        {
            return IntegrationService.RestCall<List<FunctionalUnit>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllFunctionalUnits, RestMethod.Get, null, null);
            
        }
    }
}
