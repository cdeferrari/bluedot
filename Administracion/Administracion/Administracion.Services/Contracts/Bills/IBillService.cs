using Administracion.DomainModel;
using Administracion.Dto.Bill;
using Administracion.Dto.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Bills
{
    public interface IBillService
    {
        List<Bill> GetAll();
        Bill GetBill(int BillId);
        Entidad CreateBill(BillRequest Bill);
        bool UpdateBill(BillRequest Bill);
        bool DeleteBill(int BillId);        
    }
}
