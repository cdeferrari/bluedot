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
        void CreateAdministration(Administration administration);
        void UpdateAdministration(Administration administration);
        void DeleteAdministration(int administrationId);
        IList<Administration> GetAll();
    }
}
