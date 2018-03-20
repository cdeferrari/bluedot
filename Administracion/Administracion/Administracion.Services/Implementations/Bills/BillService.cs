using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.Bills;
using Administracion.Dto.Bill;

namespace Administracion.Services.Implementations.Bills
{
    public class BillService : IBillService
    {
        public ISync IntegrationService { get; set; }

        public Entidad CreateBill(BillRequest Bill)
        {
            return IntegrationService.RestCall<Entidad>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateBill, RestMethod.Post, null, Bill);                        
        }

        public bool UpdateBill(BillRequest Bill)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateBill, Bill.Id), RestMethod.Put, null, Bill);
        }

        public bool DeleteBill(int BillId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteBill, BillId), RestMethod.Delete, null, new RestParamList { new RestParam("id", BillId) });                        
        }

        public Bill GetBill(int BillId)
        {

            return IntegrationService.RestCall<Bill>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetBill, BillId), RestMethod.Get, null, new RestParamList { new RestParam("id", BillId) });
            
        }

        public List<Bill> GetAll()
        {
            return IntegrationService.RestCall<List<Bill>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllBills, RestMethod.Get, null, null);            
        }        

    }
}
