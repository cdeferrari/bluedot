using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.TicketHistory;

namespace ApiCore.Services.Implementations.TicketHistory
{
    public class TicketHistoryService : ITicketHistoryService
    {
        public ITicketHistoryRepository TicketHistoryRepository { get; set; }
        public ITicketRepository TicketRepository { get; set; }

        [Transaction]
        public DomainModel.TicketHistory CreateTicketHistory(TicketHistoryRequest TicketHistory)
        {
            var entityToInsert = new DomainModel.TicketHistory() { };
            MergeTicketHistory(entityToInsert, TicketHistory);
            TicketHistoryRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public DomainModel.TicketHistory GetById(int TicketHistoryId)
        {
            var TicketHistory = TicketHistoryRepository.GetById(TicketHistoryId);
            if (TicketHistory == null)
                throw new BadRequestException(ErrorMessages.TicketHistoryNoEncontrado);

            return TicketHistory;
        }


        [Transaction]
        public DomainModel.TicketHistory UpdateTicketHistory(DomainModel.TicketHistory originalTicketHistory, TicketHistoryRequest TicketHistory)
        {
            this.MergeTicketHistory(originalTicketHistory, TicketHistory);
            TicketHistoryRepository.Update(originalTicketHistory);
            return originalTicketHistory;

        }


        [Transaction]
        public void DeleteTicketHistory(int TicketHistoryId)
        {
            var TicketHistory = TicketHistoryRepository.GetById(TicketHistoryId);
            TicketHistoryRepository.Delete(TicketHistory);
        }

        [Transaction]
        public IList<DomainModel.TicketHistory> GetAll()
        {
            var TicketHistorys = TicketHistoryRepository.GetAll();
            if (TicketHistorys == null)
                throw new BadRequestException(ErrorMessages.TicketHistoryNoEncontrado);

            var result = new List<DomainModel.TicketHistory>();
            var enumerator = TicketHistorys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }

        private void MergeTicketHistory(DomainModel.TicketHistory originalTicketHistory, TicketHistoryRequest TicketHistory)
        {
            originalTicketHistory.Coment = TicketHistory.Coment;
            originalTicketHistory.FollowDate = TicketHistory.FollowDate;
            originalTicketHistory.Ticket = this.TicketRepository.GetById(TicketHistory.TicketId);            
        }
        
    }

}
