using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Managers
{
    public interface IManagerService
    {
        Manager CreateManager(ManagerRequest Manager);
        Manager GetById(int ManagerId);        
        Manager UpdateManager(Manager originalManager, ManagerRequest Manager);
        void DeleteManager(int ManagerId);
        List<Manager> GetAll();
    }
}
