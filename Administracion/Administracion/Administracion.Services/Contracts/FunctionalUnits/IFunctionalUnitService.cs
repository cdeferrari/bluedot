using Administracion.DomainModel;
using Administracion.Dto.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.FunctionalUnits
{
    public interface IFunctionalUnitService
    {
        IList<FunctionalUnit> GetAll();
        FunctionalUnit GetFunctionalUnit(int functionalUnitId);
        Entidad CreateFunctionalUnit(FunctionalUnitRequest functionalUnit);
        bool UpdateFunctionalUnit(FunctionalUnitRequest functionalUnit);
        bool DeleteFunctionalUnit(int functionalUnitId);
    }
}
