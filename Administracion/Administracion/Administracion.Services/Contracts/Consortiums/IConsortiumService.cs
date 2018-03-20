using Administracion.DomainModel;
using Administracion.Dto.Consortium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Consortiums
{
    public interface IConsortiumService
    {
        List<Consortium> GetAll();
        Consortium GetConsortium(int consortiumId);
        bool CreateConsortium(ConsortiumRequest consortium);
        bool UpdateConsortium(ConsortiumRequest consortium);
        bool DeleteConsortium(int consortiumId);
        IList<List> GetAllChecklists(int consortiumId);
        bool CloseMonth(int ConsortiumId);
    }
}
