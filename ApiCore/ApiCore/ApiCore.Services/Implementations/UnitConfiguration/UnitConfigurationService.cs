using ApiCore.Services.Contracts.UnitConfigurations;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.UnitConfigurations
{
    public class UnitConfigurationService : IUnitConfigurationsService
    {
        public IUnitConfigurationRepository UnitConfigurationRepository { get; set; }
        public IUnitConfigurationTypeRepository UnitConfigurationTypeRepository { get; set; }
        public IFunctionalUnitRepository UnitRepository { get; set; }        
        
        
        [Transaction]
        public UnitConfiguration CreateUnitConfiguration(UnitConfigurationRequest UnitConfiguration)
        {
            var entityToInsert = new UnitConfiguration() { };
            MergeUnitConfiguration(entityToInsert, UnitConfiguration);
            UnitConfigurationRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public UnitConfiguration GetById(int UnitConfigurationId)
        {
            var UnitConfiguration = UnitConfigurationRepository.GetById(UnitConfigurationId);
            if (UnitConfiguration == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return UnitConfiguration;
        }
        

        [Transaction]
        public UnitConfiguration UpdateUnitConfiguration(UnitConfiguration originalUnitConfiguration, UnitConfigurationRequest UnitConfiguration)
        {            
            this.MergeUnitConfiguration(originalUnitConfiguration, UnitConfiguration);
            UnitConfigurationRepository.Update(originalUnitConfiguration);
            return originalUnitConfiguration;

        }
        

        [Transaction]
        public void DeleteUnitConfiguration(int UnitConfigurationId)
        {
            var UnitConfiguration = UnitConfigurationRepository.GetById(UnitConfigurationId);
            UnitConfigurationRepository.Delete(UnitConfiguration);
        }

        [Transaction]
        public IList<UnitConfiguration> GetAll()
        {
            var UnitConfigurations = UnitConfigurationRepository.GetAll();
            if (UnitConfigurations == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            var result = new List<UnitConfiguration>();
            var enumerator = UnitConfigurations.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

        private void MergeUnitConfiguration(UnitConfiguration originalUnitConfiguration, UnitConfigurationRequest UnitConfiguration)
        {            
            originalUnitConfiguration.Unit = this.UnitRepository.GetById(UnitConfiguration.UnitId);
            originalUnitConfiguration.Type = this.UnitConfigurationTypeRepository.GetById(UnitConfiguration.UnitConfigurationTypeId);
            originalUnitConfiguration.Value = UnitConfiguration.Value;            
            originalUnitConfiguration.ConfigurationDate = UnitConfiguration.UnitConfigurationDate;            
        }

        public IList<UnitConfiguration> GetByUnitId(int UnitId, DateTime startDate, DateTime endDate)
        {
            return UnitConfigurationRepository.GetByUnitId(UnitId, startDate, endDate).ToList();
            
        }
    }
}
