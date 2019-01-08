using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.ManagerPositions
{
    public interface IManagerPositionService
    {
        IList<ManagerPosition> GetAll();        
         //bool DeleteManagerPosition(int ManagerPositionId);
    }
}
