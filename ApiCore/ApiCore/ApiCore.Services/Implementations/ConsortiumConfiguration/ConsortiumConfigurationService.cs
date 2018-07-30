using ApiCore.Services.Contracts.ConsortiumConfigurations;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.ConsortiumConfigurations
{
    public class ConsortiumConfigurationService : IConsortiumConfigurationsService
    {
        public IConsortiumConfigurationRepository ConsortiumConfigurationRepository { get; set; }
        public IConsortiumConfigurationTypeRepository ConsortiumConfigurationTypeRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }        
        
        
        [Transaction]
        public ConsortiumConfiguration CreateConsortiumConfiguration(ConsortiumConfigurationRequest ConsortiumConfiguration)
        {
            var entityToInsert = new ConsortiumConfiguration() { };
            MergeConsortiumConfiguration(entityToInsert, ConsortiumConfiguration);
            ConsortiumConfigurationRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public ConsortiumConfiguration GetById(int ConsortiumConfigurationId)
        {
            var ConsortiumConfiguration = ConsortiumConfigurationRepository.GetById(ConsortiumConfigurationId);
            if (ConsortiumConfiguration == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return ConsortiumConfiguration;
        }
        

        [Transaction]
        public ConsortiumConfiguration UpdateConsortiumConfiguration(ConsortiumConfiguration originalConsortiumConfiguration, ConsortiumConfigurationRequest ConsortiumConfiguration)
        {            
            this.MergeConsortiumConfiguration(originalConsortiumConfiguration, ConsortiumConfiguration);
            ConsortiumConfigurationRepository.Update(originalConsortiumConfiguration);
            return originalConsortiumConfiguration;

        }
        

        [Transaction]
        public void DeleteConsortiumConfiguration(int ConsortiumConfigurationId)
        {
            var ConsortiumConfiguration = ConsortiumConfigurationRepository.GetById(ConsortiumConfigurationId);
            ConsortiumConfigurationRepository.Delete(ConsortiumConfiguration);
        }

        [Transaction]
        public IList<ConsortiumConfiguration> GetAll()
        {
            var ConsortiumConfigurations = ConsortiumConfigurationRepository.GetAll();
            if (ConsortiumConfigurations == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<ConsortiumConfiguration>();
            var enumerator = ConsortiumConfigurations.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

        private void MergeConsortiumConfiguration(ConsortiumConfiguration originalConsortiumConfiguration, ConsortiumConfigurationRequest ConsortiumConfiguration)
        {            
            originalConsortiumConfiguration.Consortium = this.ConsortiumRepository.GetById(ConsortiumConfiguration.ConsortiumId);
            originalConsortiumConfiguration.Type = this.ConsortiumConfigurationTypeRepository.GetById(ConsortiumConfiguration.ConsortiumConfigurationTypeId);
            originalConsortiumConfiguration.Value = ConsortiumConfiguration.Value;            
            originalConsortiumConfiguration.ConfigurationDate = ConsortiumConfiguration.ConsortiumConfigurationDate;            
        }

        public IList<ConsortiumConfiguration> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate)
        {
            return ConsortiumConfigurationRepository.GetByConsortiumId(consortiumId, startDate, endDate).ToList();
            
        }
    }
}
