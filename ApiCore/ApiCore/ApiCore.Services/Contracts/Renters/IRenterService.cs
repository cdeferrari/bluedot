using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Renters
{
    public interface IRenterService
    {
        Renter CreateRenter(RenterRequest Renter);
        Renter GetById(int RenterId);        
        Renter UpdateRenter(Renter originalRenter, RenterRequest Renter);
        Renter Update(Renter renter);
        void DeleteRenter(int RenterId);
        List<Renter> GetAll();
    }
}
