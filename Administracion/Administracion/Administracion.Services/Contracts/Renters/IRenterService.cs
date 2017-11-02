using Administracion.DomainModel;
using Administracion.Dto.Renter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Renters
{
    public interface IRenterService
    {
        IList<Renter> GetAll();
        Renter GetRenter(int RenterId);
        bool CreateRenter(RenterRequest Renter);
        bool UpdateRenter(Renter Renter);
        bool DeleteRenter(int RenterId);
    }
}
