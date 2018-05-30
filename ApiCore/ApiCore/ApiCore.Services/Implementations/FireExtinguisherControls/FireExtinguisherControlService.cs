using ApiCore.Services.Contracts.FireExtinguisherControls;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;

namespace ApiCore.Services.Implementations.FireExtinguisherControls
{
    public class FireExtinguisherControlService : IFireExtinguisherControlService
    {
        public IFireExtinguisherControlRepository FireExtinguisherControlRepository { get; set; }        
        public IConsortiumRepository ConsortiumRepository { get; set; }
        public IProviderRepository ProviderRepository { get; set; }

        [Transaction]
        public FireExtinguisherControl CreateFireExtinguisherControl(ControlRequest FireExtinguisherControl)
        {
            var entityToInsert = new FireExtinguisherControl() { };
            MergeFireExtinguisherControl(entityToInsert, FireExtinguisherControl);
            FireExtinguisherControlRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public FireExtinguisherControl GetById(int FireExtinguisherControlId)
        {
            var FireExtinguisherControl = FireExtinguisherControlRepository.GetById(FireExtinguisherControlId);
            if (FireExtinguisherControl == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return FireExtinguisherControl;
        }
        

        [Transaction]
        public FireExtinguisherControl UpdateFireExtinguisherControl(FireExtinguisherControl originalFireExtinguisherControl, ControlRequest FireExtinguisherControl)
        {            
            this.MergeFireExtinguisherControl(originalFireExtinguisherControl, FireExtinguisherControl);
            FireExtinguisherControlRepository.Update(originalFireExtinguisherControl);
            return originalFireExtinguisherControl;

        }
        

        [Transaction]
        public void DeleteFireExtinguisherControl(int FireExtinguisherControlId)
        {
            var FireExtinguisherControl = FireExtinguisherControlRepository.GetById(FireExtinguisherControlId);
            FireExtinguisherControlRepository.Delete(FireExtinguisherControl);
        }

        [Transaction]
        public IList<FireExtinguisherControl> GetAll()
        {
            var FireExtinguisherControl = FireExtinguisherControlRepository.GetAll();
            if (FireExtinguisherControl == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<FireExtinguisherControl>();
            var enumerator = FireExtinguisherControl.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }

        
        private void MergeFireExtinguisherControl(FireExtinguisherControl originalFireExtinguisherControl, ControlRequest FireExtinguisherControl)
        {            
            originalFireExtinguisherControl.Consortium = this.ConsortiumRepository.GetById(FireExtinguisherControl.ConsortiumId);
            originalFireExtinguisherControl.Provider = this.ProviderRepository.GetById(FireExtinguisherControl.ProviderId);
            originalFireExtinguisherControl.ControlDate = FireExtinguisherControl.ControlDate;
            originalFireExtinguisherControl.ExpirationDate = FireExtinguisherControl.ExpirationDate;
            
        }

    }
}
