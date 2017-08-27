using ApiCore.Services.Contracts.Users;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;

namespace ApiCore.Services.Implementations.Users
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }

        [Transaction]
        public User CreateUser(UserRequest user)
        {
            var entityToInsert = new User() { };
            UserRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public User GetById(int userId)
        {
            var user = UserRepository.GetById(userId);
            if (user == null)
                throw new BadRequestException(ErrorMessages.UserNoEncontrado);
            
            return user;

        }
        

        [Transaction]
        public User UpdateUser(User originalUser, UserRequest user)
        {            
            this.MergeUser(originalUser, user);
            UserRepository.Update(originalUser);
            return originalUser;

        }
        

        [Transaction]
        public void DeleteUser(int userId)
        {
            var user = UserRepository.GetById(userId);
            UserRepository.Delete(user);
        }

        #region private Methods 

        private void MergeUser(User originalUser, UserRequest User)
        {
        
        }

        #endRegion

    }
}
