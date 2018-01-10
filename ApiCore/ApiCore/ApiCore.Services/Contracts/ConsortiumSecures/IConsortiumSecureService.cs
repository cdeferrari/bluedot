using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.ConsortiumSecures
{
    public interface IConsortiumSecureService
    {
        ConsortiumSecure GetById(int unitId);        
        ConsortiumSecure CreateConsortiumSecure(ConsortiumSecureRequest request);
        void DeleteConsortiumSecure(int secureId);
        ConsortiumSecure UpdateConsortiumSecure(ConsortiumSecure originalConsortiumSecure, ConsortiumSecureRequest consortiumSecure);
        List<ConsortiumSecure> GetAll();

    }
}
