using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Consortiums;
using AutoMapper;
using System.Collections.Generic;

namespace ApiCore.Services.Implementations.Consortiums
{
    public class ConsortiumService : IConsortiumService
    {
        
        public IConsortiumRepository ConsortiumRepository { get; set; }        
        public IStatusRepository StatusRepository { get; set; }
        public IFunctionalUnitRepository FunctionalUnitRepository { get; set; }
        public IPriorityRepository PriorityRepository { get; set; }        
        public IBacklogUserRepository BacklogUserRepository { get; set; }
        public IAdministrationRepository AdministrationRepository { get; set; }
        public IOwnershipRepository OwnershipRepository { get; set; }

        [Transaction]
        public Consortium CreateConsortium(ConsortiumRequest consortium)
        {
            Consortium originalConsortium = new Consortium();
            var entityToInsert = MergeConsortium(originalConsortium, consortium);

            ConsortiumRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Consortium GetById(int consortiumId)
        {
            var consortium = ConsortiumRepository.GetById(consortiumId);
            if (consortium == null)
                throw new BadRequestException(ErrorMessages.ConsorcioNoEncontrado);

            return consortium;
        }
        

        [Transaction]
        public Consortium UpdateConsortium(Consortium originalConsortium, ConsortiumRequest consortium)
        {            
            originalConsortium = this.MergeConsortium(originalConsortium, consortium);
            ConsortiumRepository.Update(originalConsortium);
            return originalConsortium;

        }
        

        [Transaction]
        public void DeleteConsortium(int consortiumId)
        {
            var consortium = ConsortiumRepository.GetById(consortiumId);
            ConsortiumRepository.Delete(consortium);
        }
        

        private Consortium MergeConsortium(Consortium originalConsortium, ConsortiumRequest consortium)
        {
                       
            originalConsortium.CUIT = consortium.CUIT;
            originalConsortium.FriendlyName = consortium.FriendlyName;
            originalConsortium.MailingList = consortium.MailingList;
            originalConsortium.Administration = this.AdministrationRepository.GetById(consortium.AdministrationId);           
            return originalConsortium;
        }

        public List<Consortium> GetAll()
        {
            var consortiums = ConsortiumRepository.GetAll();
            if (consortiums == null)
                throw new BadRequestException(ErrorMessages.ConsorcioNoEncontrado);

            var result = new List<DomainModel.Consortium>();
            var enumerator = consortiums.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }
    }
}
