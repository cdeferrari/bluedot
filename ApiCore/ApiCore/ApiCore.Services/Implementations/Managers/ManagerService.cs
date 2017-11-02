using ApiCore.Services.Contracts.Managers;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using ApiCore.Services.Contracts.Managers;

namespace ApiCore.Services.Implementations.Managers
{
    public class ManagerService : IManagerService
    {
        public virtual IManagerRepository ManagerRepository { get; set; }

        public virtual ILaboralUnionRepository LaboralUnionRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public IAdministrationRepository AdministrationRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }



        [Transaction]
        public Manager CreateManager(ManagerRequest Manager)
        {
            var entityToInsert = new Manager()
            {
                User = this.UserRepository.GetById(Manager.UserId),
                Consortium = this.ConsortiumRepository.GetById(Manager.ConsortiumId),
                Home = Manager.Home,
                IsAlternate = Manager.IsAlternate,
                JobDomicile = Manager.JobDomicile,
                LaborUnion = this.LaboralUnionRepository.GetById(Manager.LaborUnionId),
                Salary = Manager.Salary,
                StartDate = Manager.StartDate,
                WorkInsurance = Manager.WorkInsurance
            };

            ManagerRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Manager GetById(int ManagerId)
        {
            var Manager = ManagerRepository.GetById(ManagerId);
            if (Manager == null)
                throw new BadRequestException(ErrorMessages.TrabajadorNoEncontrado);
            
            return Manager;
        }
        

        [Transaction]
        public Manager UpdateManager(Manager originalManager, ManagerRequest Manager)
        {            
            this.MergeManager(originalManager, Manager);
            ManagerRepository.Update(originalManager);
            return originalManager;
        }
        

        [Transaction]
        public void DeleteManager(int ManagerId)
        {
            var Manager = ManagerRepository.GetById(ManagerId);
            ManagerRepository.Delete(Manager);
        }
        

        private void MergeManager(Manager originalManager, ManagerRequest Manager)
        {
            originalManager.User = this.UserRepository.GetById(Manager.UserId);
            originalManager.Consortium = this.ConsortiumRepository.GetById(Manager.ConsortiumId);
            originalManager.Home = Manager.Home;
            originalManager.IsAlternate = Manager.IsAlternate;
            originalManager.JobDomicile = Manager.JobDomicile;
            originalManager.LaborUnion = this.LaboralUnionRepository.GetById(Manager.LaborUnionId);
            originalManager.Salary = Manager.Salary;
            originalManager.StartDate = Manager.StartDate;
            originalManager.WorkInsurance = Manager.WorkInsurance;
        }

        [Transaction]
        public List<Manager> GetAll()
        {
            var users = ManagerRepository.GetAll();
            if (users == null)
                throw new BadRequestException(ErrorMessages.UserNoEncontrado);

            var result = new List<Manager>();
            var enumerator = users.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }

    }
}

