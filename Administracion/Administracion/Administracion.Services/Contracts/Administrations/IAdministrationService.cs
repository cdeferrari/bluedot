using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Administrations
{
    public interface IAdministrationService
    {
        Administration GetAdministration(int administrationId);
        bool CreateAdministration(Administration administration);
        bool UpdateAdministration(Administration administration);
        bool DeleteAdministration(int administrationId);
        IList<Administration> GetAll();
    }
}
