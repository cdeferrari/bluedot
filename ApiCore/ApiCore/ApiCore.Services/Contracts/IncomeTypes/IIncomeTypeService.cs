using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System.Collections.Generic;

namespace ApiCore.Services.Contracts.IncomeTypes
{
    public interface IIncomeTypeService
    {
        IncomeType GetById(int IncomeId);
        IList<IncomeType> GetAll();
        IncomeType CreateIncomeType(IncomeTypeRequest IncomeType);
        IncomeType UpdateIncomeType(IncomeType originalIncome, IncomeTypeRequest Income);
        void Delete(int IncomeTypeId);
    }
}
