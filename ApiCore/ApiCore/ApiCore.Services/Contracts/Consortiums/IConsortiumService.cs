using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Consortiums
{
    public interface IConsortiumService
    {
        Consortium CreateConsortium(ConsortiumRequest consortium);
        Consortium GetById(int consortiumId);
        Consortium UpdateConsortium(Consortium originalConsortium, ConsortiumRequest consortium);
        void DeleteConsortium(int consortiumId);
    }
}
