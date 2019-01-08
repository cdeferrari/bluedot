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
using System.Linq;

namespace ApiCore.Services.Implementations.Managers
{
    public class ManagerService : IManagerService
    {
        public virtual IManagerRepository ManagerRepository { get; set; }
        public virtual IManagerPositionRepository ManagerPositionRepository { get; set; }

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
                Consortium = Manager.ConsortiumId != 0 ? this.ConsortiumRepository.GetById(Manager.ConsortiumId) : null,
                Home = Manager.Home,
                Schedule = Manager.Schedule,
                IsAlternate = Manager.IsAlternate,
                Male = Manager.Male,
                LaborUnion = this.LaboralUnionRepository.GetById(Manager.LaborUnionId),
                Salary = Manager.Salary,
                StartDate = Manager.StartDate,
                BirthDate = Manager.BirthDate,
                ExtraHourValue = Manager.ExtraHourValue,
                PantsWaist = Manager.PantsWaist,
                ShirtWaist = Manager.ShirtWaist,
                FootwearWaist = Manager.FootwearWaist,
                ManagerPosition = Manager.ManagerPositionId.HasValue ? this.ManagerPositionRepository.GetById(Manager.ManagerPositionId.Value) : null,
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
            originalManager.Consortium = Manager.ConsortiumId != 0 ? this.ConsortiumRepository.GetById(Manager.ConsortiumId) : null;
            originalManager.Home = Manager.Home;
            originalManager.IsAlternate = Manager.IsAlternate;
            originalManager.Male = Manager.Male;
            originalManager.LaborUnion = this.LaboralUnionRepository.GetById(Manager.LaborUnionId);
            originalManager.Salary = Manager.Salary;
            originalManager.StartDate = Manager.StartDate;
            originalManager.BirthDate = Manager.BirthDate;
            originalManager.WorkInsurance = Manager.WorkInsurance;
            originalManager.ExtraHourValue = Manager.ExtraHourValue;
            originalManager.PantsWaist = Manager.PantsWaist;
            originalManager.ShirtWaist = Manager.ShirtWaist;
            originalManager.FootwearWaist = Manager.FootwearWaist;
            originalManager.Schedule = Manager.Schedule;
            originalManager.ManagerPosition = Manager.ManagerPositionId.HasValue ? this.ManagerPositionRepository.GetById(Manager.ManagerPositionId.Value) : null;
        }

        [Transaction]
        public List<Manager> GetAll()
        {
            return ManagerRepository.GetAll().ToList();
        }

    }
}

