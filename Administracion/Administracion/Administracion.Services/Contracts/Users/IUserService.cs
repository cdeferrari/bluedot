using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Users
{
    public interface IUserService
    {
        IList<User> GetAll();
        User GetUser(int userId);
        User CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
    }
}
