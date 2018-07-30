using Administracion.DomainModel;
using Administracion.Dto.UnitConfigurations;
using System;
using System.Collections.Generic;

namespace Administracion.Services.Contracts.UnitConfigurations
{
    public interface IUnitConfigurationService
    {
        UnitConfiguration GetUnitConfiguration(int UnitConfigurationId);
        bool CreateUnitConfiguration(UnitConfigurationRequest UnitConfiguration);
        bool UpdateUnitConfiguration(UnitConfigurationRequest UnitConfiguration);
        bool DeleteUnitConfiguration(int UnitConfigurationId);
        IList<UnitConfiguration> GetByUnitId(int UnitId, DateTime startDate, DateTime endDate);
    }
}
