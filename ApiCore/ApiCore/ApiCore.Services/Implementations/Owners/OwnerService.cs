using ApiCore.Services.Contracts.Owners;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;

namespace ApiCore.Services.Implementations.Owners
{
    public class OwnerService : IOwnerService
    {
        public IOwnerRepository OwnerRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IFunctionalUnitRepository FunctionalUnitRepository { get; set; }

        [Transaction]
        public Owner CreateOwner(OwnerRequest Owner)
        {
            var entityToInsert = new Owner()
            {
                User = this.UserRepository.GetById(Owner.UserId),
                FunctionalUnit = this.FunctionalUnitRepository.GetById(Owner.FunctionalUnitId)
            };
            OwnerRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Owner GetById(int OwnerId)
        {
            var Owner = OwnerRepository.GetById(OwnerId);
            if (Owner == null)
                throw new BadRequestException(ErrorMessages.TrabajadorNoEncontrado);
            
            return Owner;

        }
        

        [Transaction]
        public Owner UpdateOwner(Owner originalOwner, OwnerRequest Owner)
        {            
            this.MergeOwner(originalOwner, Owner);
            OwnerRepository.Update(originalOwner);
            return originalOwner;

        }
        

        [Transaction]
        public void DeleteOwner(int OwnerId)
        {
            var Owner = OwnerRepository.GetById(OwnerId);
            OwnerRepository.Delete(Owner);
        }
        

        private void MergeOwner(Owner originalOwner, OwnerRequest Owner)
        {
            originalOwner.User = this.UserRepository.GetById(Owner.UserId);        
        }

        [Transaction]
        public List<Owner> GetAll()
        {
            var users = OwnerRepository.GetAll();
            if (users == null)
                throw new BadRequestException(ErrorMessages.UserNoEncontrado);

            var result = new List<Owner>();
            var enumerator = users.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }

    }
}
