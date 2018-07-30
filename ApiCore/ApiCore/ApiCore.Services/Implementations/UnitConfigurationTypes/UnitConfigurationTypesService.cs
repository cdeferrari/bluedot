using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.UnitConfigurationTypes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;

namespace ApiCore.Services.Implementations.UnitConfigurationTypes
{

    public class UnitConfigurationTypesService : IUnitConfigurationTypesService
    {
        public IUnitConfigurationTypeRepository UnitConfigurationTypeRepository { get; set; } 
        

        public UnitConfigurationType GetById(int UnitConfigurationTypeId)
        {
            var UnitConfiguration = UnitConfigurationTypeRepository.GetById(UnitConfigurationTypeId);
            if (UnitConfiguration == null)
                throw new BadRequestException(ErrorMessages.GastoNoEncontrado);

            return UnitConfiguration;
        }
        
        [Transaction]
        public IList<UnitConfigurationType> GetAll()
        {
            return UnitConfigurationTypeRepository.GetAll().ToList();
            
        }
        
    }
}
