using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Administrations;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Administrations
{
    public class AdministrationService : IAdministrationService
    {
        
        public IAdministrationRepository AdministrationRepository { get; set; }                

        [Transaction]
        public Administration CreateAdministration(AdministrationRequest Administration)
        {
            var entityToInsert = new Administration() { };
            MergeAdministration(entityToInsert, Administration);
            AdministrationRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Administration GetById(int AdministrationId)
        {
            var Administration = AdministrationRepository.GetById(AdministrationId);
            if (Administration == null)
                throw new BadRequestException(ErrorMessages.AdministracionNoEncontrado);

            return Administration;
        }
        

        [Transaction]
        public Administration UpdateAdministration(Administration originalAdministration, AdministrationRequest Administration)
        {            
            this.MergeAdministration(originalAdministration, Administration);
            AdministrationRepository.Update(originalAdministration);
            return originalAdministration;

        }
        

        [Transaction]
        public void DeleteAdministration(int AdministrationId)
        {
            var Administration = AdministrationRepository.GetById(AdministrationId);
            AdministrationRepository.Delete(Administration);
        }

        [Transaction]
        public IList<Administration> GetAll()
        {
            return this.AdministrationRepository.GetAll().ToList();
        }


        private void MergeAdministration(Administration originalAdministration, AdministrationRequest Administration)
        {
            originalAdministration.Address = Administration.Address;
            originalAdministration.CUIT = Administration.CUIT;
            originalAdministration.Name = Administration.Name;
            originalAdministration.StartDate = Administration.StartDate;            
        }


    }
}
