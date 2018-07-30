using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.UnitConfigurations
{
    public interface IUnitConfigurationsService
    {
        UnitConfiguration CreateUnitConfiguration(UnitConfigurationRequest UnitConfiguration);
        UnitConfiguration GetById(int id);
        IList<UnitConfiguration> GetAll();
        IList<UnitConfiguration> GetByUnitId(int UnitId, DateTime startDate, DateTime endDate);
        UnitConfiguration UpdateUnitConfiguration(UnitConfiguration originalUnitConfiguration, UnitConfigurationRequest UnitConfiguration);
        void DeleteUnitConfiguration(int id);
    }
}
