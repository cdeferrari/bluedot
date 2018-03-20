using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.PatrimonyStatuss
{
    public interface IPatrimonyStatusService
    {
        PatrimonyStatus CreatePatrimonyStatus(PatrimonyStatusRequest PatrimonyStatus);
        PatrimonyStatus GetById(int PatrimonyStatusId);
        IList<PatrimonyStatus> GetAll();
        IList<PatrimonyStatus> GetByConsortiumId(int consortiumId);
        PatrimonyStatus UpdatePatrimonyStatus(PatrimonyStatus originalPatrimonyStatus, PatrimonyStatusRequest PatrimonyStatus);
        void DeletePatrimonyStatus(int PatrimonyStatusId);
        bool RegisterMonth(int consortiumId);
    }
}
