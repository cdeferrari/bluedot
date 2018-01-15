using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Ownerships
{
    public interface IOwnershipService
    {
        IList<Ownership> GetAll();
        Ownership GetOwnership(int ownershipId);
        Entidad CreateOwnership(Ownership ownership);
        bool UpdateOwnership(Ownership ownership);
        bool DeleteOwnership(int ownershipId);
        IList<FunctionalUnit> GetUnits(int OwnershipId);
    }
}
