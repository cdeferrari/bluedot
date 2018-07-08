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
                PaymentTypeId = Owner.PaymentTypeId,
                FunctionalUnits = new List<FunctionalUnit>()                
            };

            if (Owner.FunctionalUnitIds.Count > 0)
            {
                foreach (var uid in Owner.FunctionalUnitIds)
                {
                    entityToInsert.FunctionalUnits.Add(this.FunctionalUnitRepository.GetById(uid));
                }                
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

            if (Owner.FunctionalUnitIds.Count > 0 )
            {
                foreach(var uid in Owner.FunctionalUnitIds)
                {
                    if(!originalOwner.FunctionalUnits.Select(x => x.Id).ToList().Contains(uid))
                    {
                        var fu = this.FunctionalUnitRepository.GetById(uid);
                        originalOwner.FunctionalUnits.Add(fu);
                    }
                }                
            }

            var oldUnitsIds = originalOwner.FunctionalUnits
                .Where(x => !Owner.FunctionalUnitIds.Contains(x.Id))
                .Select(y => y.Id).ToList();

            foreach (var unidadId in oldUnitsIds)
            {
                var fu = this.FunctionalUnitRepository.GetById(unidadId);
                fu.Owner = null;
                originalOwner.FunctionalUnits.Remove(fu);                
            }


            originalOwner.PaymentTypeId = Owner.PaymentTypeId;
        }

        [Transaction]
        public List<Owner> GetAll()
        {
            return OwnerRepository.GetAll().ToList();                        
        }

    }
}
