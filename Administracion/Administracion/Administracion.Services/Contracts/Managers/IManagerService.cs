using Administracion.DomainModel;
using Administracion.Dto.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Managers
{
    public interface IManagerService
    {
        IList<Manager> GetAll();
        Manager GetManager(int ManagerId);
        Entidad CreateManager(ManagerRequest Manager);
        bool UpdateManager(ManagerRequest Manager);
        bool DeleteManager(int ManagerId);
    }
}
