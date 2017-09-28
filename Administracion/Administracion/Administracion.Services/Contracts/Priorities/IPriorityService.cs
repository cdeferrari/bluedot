using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Priorities
{
    public interface IPriorityService
    {
        IList<Priority> GetAll();
        
    }
}
