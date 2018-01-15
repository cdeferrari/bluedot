using ApiCore.Services.Contracts.Renters;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using ApiCore.Services.Contracts.Renters;
using System.Linq;

namespace ApiCore.Services.Implementations.Renters
{
    public class RenterService : IRenterService
    { 
        public IRenterRepository RenterRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IFunctionalUnitRepository FunctionalUnitRepository { get; set; }

        [Transaction]
        public Renter CreateRenter(RenterRequest Renter)
        {
            var entityToInsert = new Renter()
            {
                User = this.UserRepository.GetById(Renter.UserId),
                PaymentTypeId =Renter.PaymentTypeId
            };

            if (Renter.FunctionalUnitId != 0)
            {
                entityToInsert.FunctionalUnitId = Renter.FunctionalUnitId;
            }
            

            if (Renter.FunctionalUnitId != 0)
            {
                entityToInsert.FunctionalUnitId = Renter.FunctionalUnitId;
            }

            RenterRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Renter GetById(int RenterId)
        {
            var Renter = RenterRepository.GetById(RenterId);
            if (Renter == null)
                throw new BadRequestException(ErrorMessages.TrabajadorNoEncontrado);
            
            return Renter;

        }
        

        [Transaction]
        public Renter UpdateRenter(Renter originalRenter, RenterRequest Renter)
        {            
            this.MergeRenter(originalRenter, Renter);
            RenterRepository.Update(originalRenter);
            return originalRenter;

        }
        

        [Transaction]
        public void DeleteRenter(int RenterId)
        {
            var Renter = RenterRepository.GetById(RenterId);
            RenterRepository.Delete(Renter);
        }
        

        private void MergeRenter(Renter originalRenter, RenterRequest Renter)
        {            
            originalRenter.User = this.UserRepository.GetById(Renter.UserId);
            originalRenter.FunctionalUnitId = Renter.FunctionalUnitId;//this.FunctionalUnitRepository.GetById(Renter.FunctionalUnitId);
            originalRenter.PaymentTypeId = Renter.PaymentTypeId;            
        }

        [Transaction]
        public List<Renter> GetAll()
        {
            return RenterRepository.GetAll().ToList();
            
        }

        [Transaction]
        public Renter Update(Renter renter)
        {
            this.RenterRepository.Update(renter);
            return renter;
        }
    }
}
