using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Owners
{
    public interface IOwnerService
    {
        Owner CreateOwner(OwnerRequest Owner);
        Owner GetById(int OwnerId);        
        Owner UpdateOwner(Owner originalOwner, OwnerRequest Owner);
        void DeleteOwner(int OwnerId);
        List<Owner> GetAll();
    }
}
