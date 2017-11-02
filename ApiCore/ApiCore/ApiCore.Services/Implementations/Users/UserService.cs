using ApiCore.Services.Contracts.Users;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;

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
                Surname = user.Surname                
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
        }


        [Transaction]
        public List<User> GetAll()
        {
            var users = UserRepository.GetAll();
            if (users == null)
                throw new BadRequestException(ErrorMessages.UserNoEncontrado);

            var result = new List<User>();
            var enumerator = users.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }
    }
}

