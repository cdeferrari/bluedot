using ApiCore.Services.Contracts.Workers;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Workers
{
    public class WorkerService : IWorkerService
    {
        public IWorkerRepository WorkerRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public IAdministrationRepository AdministrationRepository { get; set; }

        

        [Transaction]
        public Worker CreateWorker(WorkerRequest Worker)
        {
            var entityToInsert = new Worker()
            {
                User = this.UserRepository.GetById(Worker.UserId)
            };
            WorkerRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Worker GetById(int WorkerId)
        {
            var Worker = WorkerRepository.GetById(WorkerId);
            if (Worker == null)
                throw new BadRequestException(ErrorMessages.TrabajadorNoEncontrado);
            
            return Worker;
        }
        

        [Transaction]
        public Worker UpdateWorker(Worker originalWorker, WorkerRequest Worker)
        {            
            this.MergeWorker(originalWorker, Worker);
            WorkerRepository.Update(originalWorker);
            return originalWorker;
        }
        

        [Transaction]
        public void DeleteWorker(int WorkerId)
        {
            var Worker = WorkerRepository.GetById(WorkerId);
            WorkerRepository.Delete(Worker);
        }
        

        private void MergeWorker(Worker originalWorker, WorkerRequest Worker)
        {
            
            originalWorker.User = this.UserRepository.GetById(Worker.UserId);
        }

        [Transaction]
        public List<Worker> GetAll()
        {
            return WorkerRepository.GetAll().ToList();
            
        }

    }
}

