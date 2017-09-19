using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;

namespace ApiCore.Services.Implementations.Consortiums
{
    public class ConsortiumService : IConsortiumService
    {
        
        public IConsortiumRepository ConsortiumRepository { get; set; }        
        public IStatusRepository StatusRepository { get; set; }
        public IFunctionalUnitRepository FunctionalUnitRepository { get; set; }
        public IPriorityRepository PriorityRepository { get; set; }        
        public IBacklogUserRepository BacklogUserRepository { get; set; }

        [Transaction]
        public Consortium CreateTicket(ConsortiumRequest ticket)
        {
            var entityToInsert = new Consortium() { };
            MergeConsortium(entityToInsert, consortium);
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
            this.MergeConsortium(originalConsortium, consortium);
            ConsortiumRepository.Update(originalConsortium);
            return originalConsortium;

        }
        

        [Transaction]
        public void DeleteConsortium(int consortiumId)
        {
            var consortium = ConsortiumRepository.GetById(consortiumId);
            ConsortiumRepository.Delete(consortium);
        }
        

        private void MergeConsortium(Consortium originalConsortium, ConsortiumRequest consortium)
        {
            
        }
        
    }
}
