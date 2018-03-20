
using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Dto;
using Administracion.Services.Contracts.IncomeTypes;
using Administracion.Dto.Income;

namespace Administracion.Services.Implementations.IncomeTypes
{
    public class IncomeTypesService : IIncomeTypeService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateIncomeType(IncomeTypeRequest IncomeType)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateIncomeType, RestMethod.Post, null, IncomeType);
            
        }

        public bool DeleteIncomeType(int id)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteIncomeType, id), RestMethod.Delete, null, new RestParamList { new RestParam("id", id) });
        }

        public List<IncomeType> GetAll()
        {
            return IntegrationService.RestCall<List<IncomeType>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllIncomeTypes, RestMethod.Get, null, null);                                    
        }

        public IncomeType GetIncomeType(int IncomeTypeId)
        {
            return IntegrationService.RestCall<IncomeType>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetIncomeType, IncomeTypeId), RestMethod.Get, null, new RestParamList { new RestParam("id", IncomeTypeId) });
        }

        public bool UpdateIncomeType(IncomeTypeRequest IncomeType)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateIncomeType, IncomeType.Id), RestMethod.Put, null, IncomeType);
        }

       
    }
}
