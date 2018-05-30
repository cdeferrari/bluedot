using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.Bills;

namespace ApiCore.Services.Implementations.Bills
{
    public class BillService : IBillService
    {
        public IBillRepository BillRepository { get; set; }                
        public IWorkerRepository WorkerRepository { get; set; }
        public IProviderRepository ProviderRepository { get; set; }
        public IManagerRepository ManagerRepository { get; set; }

        [Transaction]
        public Bill CreateBill(BillRequest Bill)
        {
            var entityToInsert = new Bill() { };
            MergeBill(entityToInsert, Bill);
            BillRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Bill GetById(int BillId)
        {
            var Bill = BillRepository.GetById(BillId);
            if (Bill == null)
                throw new BadRequestException(ErrorMessages.FacturaNoEncontrada);

            return Bill;
        }
        

        [Transaction]
        public Bill UpdateBill(Bill originalBill, BillRequest Bill)
        {            
            this.MergeBill(originalBill, Bill);
            BillRepository.Update(originalBill);
            return originalBill;

        }
        

        [Transaction]
        public void DeleteBill(int BillId)
        {
            var Bill = BillRepository.GetById(BillId);
            BillRepository.Delete(Bill);
        }

        [Transaction]
        public IList<Bill> GetAll()
        {
            var Bills = BillRepository.GetAll();
            if (Bills == null)
                throw new BadRequestException(ErrorMessages.FacturaNoEncontrada);

            var result = new List<Bill>();
            var enumerator = Bills.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

        private void MergeBill(Bill originalBill, BillRequest Bill)
        {
            originalBill.Amount = Bill.Amount;
            originalBill.CreationDate = Bill.CreationDate;
            originalBill.ExpirationDate = Bill.ExpirationDate;
            originalBill.NextExpirationDate = Bill.NextExpirationDate;
            originalBill.Number = Bill.Number;
            originalBill.ClientNumber = Bill.ClientNumber;
            originalBill.Worker = Bill.WorkerId.HasValue ? this.WorkerRepository.GetById(Bill.WorkerId.Value) : null;
            originalBill.Provider = Bill.ProviderId.HasValue ? this.ProviderRepository.GetById(Bill.ProviderId.Value) : null;
            originalBill.Manager = Bill.ManagerId.HasValue ? this.ManagerRepository.GetById(Bill.ManagerId.Value) : null;
            
        }

        
    }
}
