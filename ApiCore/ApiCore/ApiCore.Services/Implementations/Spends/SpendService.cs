using ApiCore.Services.Contracts.Spends;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.Consortiums;

namespace ApiCore.Services.Implementations.Spends
{
    public class SpendService : ISpendService
    {
        public ISpendRepository SpendRepository { get; set; }
        public ISpendTypeRepository SpendTypeRepository { get; set; }
        public ISpendClassRepository SpendClassRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }
        public IBillRepository BillRepository { get; set; }
        public ITaskRepository TaskRepository { get; set; }

        [Transaction]
        public Spend CreateSpend(SpendRequest Spend)
        {
            var entityToInsert = new Spend() { };
            MergeSpend(entityToInsert, Spend);
            SpendRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Spend GetById(int SpendId)
        {
            var Spend = SpendRepository.GetById(SpendId);
            if (Spend == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return Spend;
        }
        

        [Transaction]
        public Spend UpdateSpend(Spend originalSpend, SpendRequest Spend)
        {            
            this.MergeSpend(originalSpend, Spend);
            SpendRepository.Update(originalSpend);
            return originalSpend;

        }
        

        [Transaction]
        public void DeleteSpend(int SpendId)
        {
            var Spend = SpendRepository.GetById(SpendId);
            SpendRepository.Delete(Spend);
        }

        [Transaction]
        public IList<Spend> GetAll()
        {
            var Spends = SpendRepository.GetAll();
            if (Spends == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<Spend>();
            var enumerator = Spends.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

        private void MergeSpend(Spend originalSpend, SpendRequest Spend)
        {
            originalSpend.Bill = this.BillRepository.GetById(Spend.BillId);
            originalSpend.Consortium = this.ConsortiumRepository.GetById(Spend.ConsortiumId);
            originalSpend.Type = this.SpendTypeRepository.GetById(Spend.SpendTypeId);
            originalSpend.SpendClass = this.SpendClassRepository.GetById(Spend.SpendClassId);
            originalSpend.Task = Spend.TaskId.HasValue ? this.TaskRepository.GetById(Spend.TaskId.Value) : null;
            originalSpend.Description = Spend.Description;
            originalSpend.PaymentDate = Spend.PaymentDate;            
        }

        public IList<Spend> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            return SpendRepository.GetByConsortiumId(consortiumId, startDate, endDate).ToList();
            
        }

        

    }
}
