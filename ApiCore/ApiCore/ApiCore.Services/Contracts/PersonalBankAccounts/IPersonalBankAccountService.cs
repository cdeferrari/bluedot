using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.BankAccounts
{
    public interface IPersonalBankAccountService
    {
        BankAccount CreateBankAccount(PersonalBankAccountRequest BankAccount);
        BankAccount GetById(int BankAccountId);        
        BankAccount UpdateBankAccount(PersonalBankAccount originalBankAccount, PersonalBankAccountRequest BankAccount);
        void DeleteBankAccount(int BankAccountId);
        List<BankAccount> GetAll();
    }
}
