using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Bills
{
    public interface IBillService
    {
        Bill CreateBill(BillRequest Bill);
        Bill GetById(int BillId);
        IList<Bill> GetAll();        
        Bill UpdateBill(Bill originalBill, BillRequest Bill);
        void DeleteBill(int BillId);
    }
}
