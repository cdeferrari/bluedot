using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System.Collections.Generic;

namespace ApiCore.Services.Contracts.ConsortiumConfigurationTypes
{
    public interface IConsortiumConfigurationTypesService
    {
        ConsortiumConfigurationType GetById(int id);
        IList<ConsortiumConfigurationType> GetAll();        
    }
}
