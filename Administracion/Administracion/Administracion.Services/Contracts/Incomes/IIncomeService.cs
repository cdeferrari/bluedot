using Administracion.DomainModel;
using Administracion.Dto.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Incomes
{
    public interface IIncomeService
    {
        List<Income> GetAll();
        Income GetIncome(int IncomeId);
        bool CreateIncome(IncomeRequest Income);
        bool UpdateIncome(IncomeRequest Income);
        bool DeleteIncome(int IncomeId);
        IList<Income> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate);
    }
}
