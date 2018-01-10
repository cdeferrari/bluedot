using System;
using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Newtonsoft.Json;
using Administracion.Services.Contracts.Lists;
using Administracion.Dto.List;
using Administracion.Services.Contracts.CommonData;
using Administracion.Dto.CommonData;

namespace Administracion.Services.Implementations.CommonData
{
    public class CommonDataService : ICommonDataService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateCommonData(CommonDataRequest CommonData)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateCommonData, RestMethod.Post, null, CommonData);                        
        }

        public bool UpdateCommonData(CommonDataRequest CommonData)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateCommonData, CommonData.Id), RestMethod.Put,  null, CommonData);                        
        }

        public bool DeleteCommonData(int CommonDataId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteCommonData, CommonDataId), RestMethod.Delete, null, new RestParamList { new RestParam("id", CommonDataId) });                        
        }

        public DomainModel.CommonData GetCommonData(int CommonDataId)
        {
            return IntegrationService.RestCall<DomainModel.CommonData>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetCommonData, CommonDataId.ToString()), RestMethod.Get, null, null);                        
            
        }

        public IList<DomainModel.CommonData> GetAll()
        {
            return IntegrationService.RestCall<List<DomainModel.CommonData>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllCommonData, RestMethod.Get, null, null);                                    
        }

        public IList<DomainModel.CommonDataItem> GetItems()
        {
            return IntegrationService.RestCall<List<DomainModel.CommonDataItem>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllCommonDataItems, RestMethod.Get, null, null);
        }
    }
}
