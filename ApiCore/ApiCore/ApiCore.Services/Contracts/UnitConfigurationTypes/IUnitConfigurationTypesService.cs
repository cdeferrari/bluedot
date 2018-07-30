using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System.Collections.Generic;

namespace ApiCore.Services.Contracts.UnitConfigurationTypes
{
    public interface IUnitConfigurationTypesService
    {
        UnitConfigurationType GetById(int id);
        IList<UnitConfigurationType> GetAll();        
    }
}
