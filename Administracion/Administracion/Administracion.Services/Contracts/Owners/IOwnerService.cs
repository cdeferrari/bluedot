using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Owners
{
    public interface IOwnerService
    {
        IList<Owner> GetAll();
        Owner GetOwner(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(int ownerId);
    }
}
