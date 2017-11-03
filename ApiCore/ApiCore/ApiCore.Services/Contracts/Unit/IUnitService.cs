using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Unit
{
    public interface IUnitService
    {
        FunctionalUnit GetById(int unitId);
        FunctionalUnit Update(FunctionalUnit unit);
        FunctionalUnit CreateUnit(FunctionalUnitRequest request);
        void DeleteUnit(int unitId);
        FunctionalUnit UpdateUnit(FunctionalUnit originalFunctionalUnit, FunctionalUnitRequest unit);
        List<FunctionalUnit> GetAll();

    }
}
