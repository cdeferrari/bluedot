using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Users
{
    public interface IUserService
    {
        User CreateUser(UserRequest user);
        User GetById(int userId);        
        User UpdateUser(User originalUser, UserRequest user);
        void DeleteUser(int userId);
    }
}
