using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.AccountStatuss
{
    public interface IAccountStatusService
    {
        AccountStatus CreateAccountStatus(AccountStatusRequest AccountStatus);
        AccountStatus GetById(int AccountStatusId);
        IList<AccountStatus> GetAll();
        IList<AccountStatus> GetByUnitId(int unitId);
        AccountStatus UpdateAccountStatus(AccountStatus originalAccountStatus, AccountStatusRequest AccountStatus);
        void DeleteAccountStatus(int AccountStatusId);
        bool RegisterMonth(int unitId, int month);
        IList<UnitAccountStatusSummary> GetConsortiumSummary(int consortiumId, int month);


    }
}
