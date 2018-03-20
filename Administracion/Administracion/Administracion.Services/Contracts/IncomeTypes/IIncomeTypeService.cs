using Administracion.DomainModel;
using Administracion.Dto.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.IncomeTypes
{
    public interface IIncomeTypeService
    {
        List<IncomeType> GetAll();
        IncomeType GetIncomeType(int IncomeTypeId);
        bool CreateIncomeType(IncomeTypeRequest IncomeType);
        bool UpdateIncomeType(IncomeTypeRequest IncomeType);
        bool DeleteIncomeType(int IncomeTypeId);
        
    }
}
