using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Status
{
    public interface IStatusService
    {
        IList<DomainModel.Status> GetAll();
        
    }
}
