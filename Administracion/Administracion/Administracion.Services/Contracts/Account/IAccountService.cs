using Administracion.DomainModel;
using Administracion.Dto.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Managers
{
    public interface IAccountService
    {
        IList<BacklogUser> GetAll();
        
    }
}
