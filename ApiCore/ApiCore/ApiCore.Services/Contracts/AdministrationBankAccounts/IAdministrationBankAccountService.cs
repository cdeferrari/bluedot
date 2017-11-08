using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.BankAccounts
{
    public interface IAdministrationBankAccountService
    {
        BankAccount CreateBankAccount(AdministrationBankAccountRequest BankAccount);
        BankAccount GetById(int BankAccountId);        
        BankAccount UpdateBankAccount(BankAccount originalBankAccount, AdministrationBankAccountRequest BankAccount);
        void DeleteBankAccount(int BankAccountId);
        List<BankAccount> GetAll();
    }
}
