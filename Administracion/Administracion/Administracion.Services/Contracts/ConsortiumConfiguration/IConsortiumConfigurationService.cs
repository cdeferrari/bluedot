using Administracion.DomainModel;
using Administracion.Dto.ConsortiumConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.ConsortiumConfigurations
{
    public interface IConsortiumConfigurationService
    {
        List<ConsortiumConfiguration> GetAll();
        ConsortiumConfiguration GetConsortiumConfiguration(int ConsortiumConfigurationId);
        bool CreateConsortiumConfiguration(ConsortiumConfigurationRequest ConsortiumConfiguration);        
        bool DeleteConsortiumConfiguration(int ConsortiumConfigurationId);
        IList<ConsortiumConfiguration> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate);
    }
}
