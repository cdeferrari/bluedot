using Administracion.DomainModel;
using Administracion.Dto.PatrimonyStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.PatrimonyStatuss
{
    public interface IPatrimonyStatusService
    {
        List<PatrimonyStatus> GetAll();
        PatrimonyStatus GetPatrimonyStatus(int PatrimonyStatusId);
        bool CreatePatrimonyStatus(PatrimonyStatusRequest PatrimonyStatus);
        bool UpdatePatrimonyStatus(PatrimonyStatusRequest PatrimonyStatus);
        bool DeletePatrimonyStatus(int PatrimonyStatusId);
        IList<PatrimonyStatus> GetByConsortiumId(int consortiumId);
    }
}
