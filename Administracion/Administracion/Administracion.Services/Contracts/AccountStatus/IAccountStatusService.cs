using Administracion.DomainModel;
using Administracion.Dto.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.AccountStatuss
{
    public interface IAccountStatusService
    {
        List<AccountStatus> GetAll();
        AccountStatus GetAccountStatus(int AccountStatusId);
        bool CreateAccountStatus(AccountStatusRequest AccountStatus);
        bool UpdateAccountStatus(AccountStatusRequest AccountStatus);
        bool DeleteAccountStatus(int AccountStatusId);
        //IList<AccountStatus> GetByUnitId(int unitId);
    }
}
