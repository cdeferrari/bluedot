using ApiCore.Services.Contracts.Workers;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;

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
                User = this.UserRepository.GetById(Worker.UserId),
                Administration = this.AdministrationRepository.GetById(Worker.ConsortiumId)
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

            originalWorker.Administration = this.AdministrationRepository.GetById(Worker.ConsortiumId);
            originalWorker.User = this.UserRepository.GetById(Worker.UserId);
        }

        [Transaction]
        public List<Worker> GetAll()
        {
            var users = WorkerRepository.GetAll();
            if (users == null)
                throw new BadRequestException(ErrorMessages.UserNoEncontrado);

            var result = new List<Worker>();
            var enumerator = users.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }

    }
}

