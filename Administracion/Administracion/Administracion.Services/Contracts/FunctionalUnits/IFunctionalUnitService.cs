using Administracion.DomainModel;
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
        bool CreateFunctionalUnit(FunctionalUnit functionalUnit);
        bool UpdateFunctionalUnit(FunctionalUnit functionalUnit);
        bool DeleteFunctionalUnit(int functionalUnitId);
    }
}
