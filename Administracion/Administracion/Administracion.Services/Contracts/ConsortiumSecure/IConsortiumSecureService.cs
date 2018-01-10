using Administracion.DomainModel;
using Administracion.Dto.ConsortiumSecure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.ConsortiumSecure
{
    public interface IConsortiumSecureService
    {
        IList<DomainModel.ConsortiumSecure> GetAll();
        DomainModel.ConsortiumSecure GetConsortiumSecure(int ConsortiumSecureId);
        Entidad CreateConsortiumSecure(ConsortiumSecureRequest ConsortiumSecure);
        bool UpdateConsortiumSecure(ConsortiumSecureRequest ConsortiumSecure);
        bool DeleteConsortiumSecure(int ConsortiumSecureId);
    }
}
