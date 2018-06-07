using ApiCore.Services.Contracts.Users;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Users
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }
        public IContactDataRepository ContactDataRepository { get; set; }

        [Transaction]
        public User CreateUser(UserRequest user)
        {

            var entityToInsert = new User()
            {
                CUIT = user.CUIT,
                DNI = user.DNI,
                Name = user.Name,
                Surname = user.Surname,
                ProfilePic = user.ProfilePic,
                Comments = user.Comments
            };

            if (user.ContactData != null)
            {
                var ndata = new ContactData()
                {
                    Cellphone = user.ContactData.Cellphone,
                    Email = user.ContactData.Email,
                    Telephone = user.ContactData.Telephone
                };
                ContactDataRepository.Insert(ndata);
                entityToInsert.ContactData = ndata;
            }

            UserRepository.Insert(entityToInsert);
            return entityToInsert;
        }


        [Transaction]
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
        

        private void MergeUser(User originalUser, UserRequest User)
        {
            originalUser.ContactData = User.ContactData;
            originalUser.CUIT = User.CUIT;
            originalUser.DNI = User.DNI;
            originalUser.Name = User.Name;
            originalUser.Surname = User.Surname;
            originalUser.ProfilePic = User.ProfilePic;
            originalUser.Comments = User.Comments;
        }


        [Transaction]
        public List<User> GetAll()
        {
            return UserRepository.GetAll().ToList();
            
        }
    }
}

