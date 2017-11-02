using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApiCore.Services.Implementations.BacklogUsers
{
    public interface IBacklogUserService
    {
         DomainModel.BacklogUser GetByEmailAndPassword(string email, string password);
        List<DomainModel.BacklogUser> GetAll();
    }
}
