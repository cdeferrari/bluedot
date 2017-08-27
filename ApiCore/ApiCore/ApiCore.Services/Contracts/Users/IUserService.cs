using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Users
{
    interface IUserService
    {
        User CreateUser(UserRequest user);
        User GetById(int userId);
        User UpdateUser(User originalUser, UserRequest user);
        void DeleteTicket(int userId);
    }
}
