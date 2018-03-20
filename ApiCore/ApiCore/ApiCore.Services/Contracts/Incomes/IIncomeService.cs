using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Incomes
{
    public interface IIncomeService
    {
        Income CreateIncome(IncomeRequest Income);
        Income GetById(int IncomeId);
        IList<Income> GetAll();
        IList<Income> GetByConsortiumId(int consortiumId, DateTime startDate, DateTime endDate);
        Income UpdateIncome(Income originalIncome, IncomeRequest Income);
        void DeleteIncome(int IncomeId);
    }
}
