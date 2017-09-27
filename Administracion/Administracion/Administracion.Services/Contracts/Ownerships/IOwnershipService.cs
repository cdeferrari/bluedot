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
        void CreateOwnership(Ownership ownership);
        void UpdateOwnership(Ownership ownership);
        void DeleteOwnership(int ownershipId);
    }
}
