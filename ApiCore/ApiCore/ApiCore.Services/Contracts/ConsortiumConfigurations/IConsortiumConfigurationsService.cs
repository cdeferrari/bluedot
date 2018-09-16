using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.ConsortiumConfigurations
{
    public interface IConsortiumConfigurationsService
    {
        ConsortiumConfiguration CreateConsortiumConfiguration(ConsortiumConfigurationRequest ConsortiumConfiguration);
        ConsortiumConfiguration GetById(int id);
        IList<ConsortiumConfiguration> GetAll();
        IList<ConsortiumConfiguration> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate);
        ConsortiumConfiguration UpdateConsortiumConfiguration(ConsortiumConfiguration originalConsortiumConfiguration, ConsortiumConfigurationRequest ConsortiumConfiguration);
        void DeleteConsortiumConfiguration(int id);
    }
}
