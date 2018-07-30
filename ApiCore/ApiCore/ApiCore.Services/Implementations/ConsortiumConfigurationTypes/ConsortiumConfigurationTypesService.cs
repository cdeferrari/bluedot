using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.ConsortiumConfigurationTypes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;

namespace ApiCore.Services.Implementations.ConsortiumConfigurationTypes
{

    public class ConsortiumConfigurationTypesService : IConsortiumConfigurationTypesService
    {
        public IConsortiumConfigurationTypeRepository ConsortiumConfigurationTypeRepository { get; set; } 
        
        public ConsortiumConfigurationType GetById(int ConsortiumConfigurationTypeId)
        {
            var ConsortiumConfiguration = ConsortiumConfigurationTypeRepository.GetById(ConsortiumConfigurationTypeId);
            if (ConsortiumConfiguration == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return ConsortiumConfiguration;
        }
        
        [Transaction]
        public IList<ConsortiumConfigurationType> GetAll()
        {
            return ConsortiumConfigurationTypeRepository.GetAll().ToList();
            
        }
        
    }
}
