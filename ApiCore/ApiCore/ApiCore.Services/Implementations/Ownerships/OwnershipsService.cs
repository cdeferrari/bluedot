using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Ownerships;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Ownerships
{
    public class OwnershipService : IOwnershipService
    {
        
        public IOwnershipRepository OwnershipRepository { get; set; }                

        [Transaction]
        public Ownership CreateOwnership(OwnershipRequest Ownership)
        {
            var entityToInsert = new Ownership() { };
            MergeOwnership(entityToInsert, Ownership);
            OwnershipRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Ownership GetById(int OwnershipId)
        {
            var Ownership = OwnershipRepository.GetById(OwnershipId);
            if (Ownership == null)
                throw new BadRequestException(ErrorMessages.PropiedadNoEncontrada);

            return Ownership;
        }
        

        [Transaction]
        public Ownership UpdateOwnership(Ownership originalOwnership, OwnershipRequest Ownership)
        {            
            this.MergeOwnership(originalOwnership, Ownership);
            OwnershipRepository.Update(originalOwnership);
            return originalOwnership;

        }
        

        [Transaction]
        public void DeleteOwnership(int OwnershipId)
        {
            var Ownership = OwnershipRepository.GetById(OwnershipId);
            OwnershipRepository.Delete(Ownership);
        }

        [Transaction]
        public IList<Ownership> GetAll()
        {
            return this.OwnershipRepository.GetAll().ToList();
        }


        private void MergeOwnership(Ownership originalOwnership, OwnershipRequest Ownership)
        {
            originalOwnership.Address = Ownership.Address;
            originalOwnership.Category = Ownership.Category;
        }


    }
}
