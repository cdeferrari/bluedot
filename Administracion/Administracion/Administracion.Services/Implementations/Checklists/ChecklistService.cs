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

namespace Administracion.Services.Implementations.Checklists
{
    public class ChecklistService : IChecklistService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateList(ListRequest Checklist)
        {
          return  IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateChecklist, RestMethod.Post, null, Checklist);                        
        }

        public bool UpdateList(ListRequest Checklist)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateChecklist, Checklist.Id), RestMethod.Put,  null, Checklist);                        
        }

        public bool DeleteList(int ChecklistId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteChecklist, ChecklistId), RestMethod.Delete, null, new RestParamList { new RestParam("id", ChecklistId) });                        
        }

        public List GetList(int ChecklistId)
        {
            return IntegrationService.RestCall<List>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetChecklist, ChecklistId.ToString()), RestMethod.Get, null, null);                        
            
        }

        public IList<List> GetAll()
        {
            return IntegrationService.RestCall<List<List>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllChecklist, RestMethod.Get, null, null);                                    
        }

        public IList<Item> GetItems()
        {
            return IntegrationService.RestCall<List<Item>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllItems, RestMethod.Get, null, null);
        }
    }
}
