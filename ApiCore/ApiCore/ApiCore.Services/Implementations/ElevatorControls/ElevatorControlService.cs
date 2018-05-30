using ApiCore.Services.Contracts.ElevatorControls;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.Spends;
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.Services.Contracts.Incomes;

namespace ApiCore.Services.Implementations.ElevatorControls
{
    public class ElevatorControlService : IElevatorControlService
    {
        public IElevatorControlRepository ElevatorControlRepository { get; set; }        
        public IConsortiumRepository ConsortiumRepository { get; set; }
        public IProviderRepository ProviderRepository { get; set; }

        [Transaction]
        public ElevatorControl CreateElevatorControl(ControlRequest ElevatorControl)
        {
            var entityToInsert = new ElevatorControl() { };
            MergeElevatorControl(entityToInsert, ElevatorControl);
            ElevatorControlRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public ElevatorControl GetById(int ElevatorControlId)
        {
            var ElevatorControl = ElevatorControlRepository.GetById(ElevatorControlId);
            if (ElevatorControl == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return ElevatorControl;
        }
        

        [Transaction]
        public ElevatorControl UpdateElevatorControl(ElevatorControl originalElevatorControl, ControlRequest ElevatorControl)
        {            
            this.MergeElevatorControl(originalElevatorControl, ElevatorControl);
            ElevatorControlRepository.Update(originalElevatorControl);
            return originalElevatorControl;

        }
        

        [Transaction]
        public void DeleteElevatorControl(int ElevatorControlId)
        {
            var ElevatorControl = ElevatorControlRepository.GetById(ElevatorControlId);
            ElevatorControlRepository.Delete(ElevatorControl);
        }

        [Transaction]
        public IList<ElevatorControl> GetAll()
        {
            var ElevatorControl = ElevatorControlRepository.GetAll();
            if (ElevatorControl == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<ElevatorControl>();
            var enumerator = ElevatorControl.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }

        
        private void MergeElevatorControl(ElevatorControl originalElevatorControl, ControlRequest ElevatorControl)
        {            
            originalElevatorControl.Consortium = this.ConsortiumRepository.GetById(ElevatorControl.ConsortiumId);
            originalElevatorControl.Provider = this.ProviderRepository.GetById(ElevatorControl.ProviderId);
            originalElevatorControl.ControlDate = ElevatorControl.ControlDate;
            originalElevatorControl.ExpirationDate = ElevatorControl.ExpirationDate;
            
        }

    }
}
