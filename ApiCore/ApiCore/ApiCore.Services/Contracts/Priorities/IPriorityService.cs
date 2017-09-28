using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Priorities
{
    public interface IPriorityService
    {
        
        IList<DomainModel.Priority> GetAll();
        
    }
}
