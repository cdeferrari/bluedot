using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Ownerships
{
    public interface IOwnershipService
    {
        Ownership CreateOwnership(OwnershipRequest Ownership);
        Ownership GetById(int OwnershipId);
        Ownership UpdateOwnership(Ownership originalOwnership, OwnershipRequest Ownership);
        void DeleteOwnership(int OwnershipId);
        IList<Ownership> GetAll();
    }
}
