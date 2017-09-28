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


namespace Administracion.Services.Implementations.FunctionalUnits
{
    public class FunctionalUnitService : IFunctionalUnitService
    {
        public ISync IntegrationService { get; set; }

        public void CreateFunctionalUnit(FunctionalUnit FunctionalUnit)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateFunctionalUnit, RestMethod.Post, null, FunctionalUnit);                        
        }

        public void UpdateFunctionalUnit(FunctionalUnit FunctionalUnit)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.UpdateFunctionalUnit, RestMethod.Put, null, new RestParamList {new RestParam("id", FunctionalUnit.Id), new RestParam("FunctionalUnit", FunctionalUnit) });                        
        }

        public void DeleteFunctionalUnit(int FunctionalUnitId)
        {
            IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.DeleteFunctionalUnit, RestMethod.Delete, null, new RestParamList { new RestParam("id", FunctionalUnitId) });                        
        }

        public FunctionalUnit GetFunctionalUnit(int FunctionalUnitId)
        {

            return IntegrationService.RestCall<FunctionalUnit>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetFunctionalUnit, RestMethod.Get, null, new RestParamList { new RestParam("id", FunctionalUnitId) });
            
        }

        public IList<FunctionalUnit> GetAll()
        {
            return IntegrationService.RestCall<List<FunctionalUnit>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetFunctionalUnit, RestMethod.Get, null, null);
            
        }
    }
}
