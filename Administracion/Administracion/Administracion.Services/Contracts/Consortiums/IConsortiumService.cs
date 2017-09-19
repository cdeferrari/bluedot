using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Consortiums
{
    public interface IConsortiumService
    {
        void CreateConsortium(Consortium consortium);
        void UpdateConsortium(Consortium consortium);
        void DeleteConsortium(int consortiumId);
    }
}
