using ApiCore.Services.Contracts.Owners;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

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
                PaymentTypeId = Owner.PaymentTypeId
                
            };

            if (Owner.FunctionalUnitId != 0)
            {
                entityToInsert.FunctionalUnitId = Owner.FunctionalUnitId;
            }

            OwnerRepository.Insert(entityToInsert);
            return entityToInsert;
        }


        [Transaction]
        public Owner Update(Owner Owner)
        {            
            OwnerRepository.Update(Owner);
            return Owner;
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
            originalOwner.FunctionalUnitId = Owner.FunctionalUnitId;// this.FunctionalUnitRepository.GetById(Owner.FunctionalUnitId);
            originalOwner.PaymentTypeId = Owner.PaymentTypeId;
        }

        [Transaction]
        public List<Owner> GetAll()
        {
            return OwnerRepository.GetAll().ToList();            
            
        }

    }
}
