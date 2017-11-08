using ApiCore.Services.Contracts.Users;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using ApiCore.Services.Contracts.BankAccounts;

namespace ApiCore.Services.Implementations.Users
{
    public class AdministrationBankAccountService : IAdministrationBankAccountService
    {
        public BankAccount CreateBankAccount(AdministrationBankAccountRequest BankAccount)
        {
            throw new NotImplementedException();
        }

        public void DeleteBankAccount(int BankAccountId)
        {
            throw new NotImplementedException();
        }

        public List<BankAccount> GetAll()
        {
            throw new NotImplementedException();
        }

        public BankAccount GetById(int BankAccountId)
        {
            throw new NotImplementedException();
        }

        public BankAccount UpdateBankAccount(BankAccount originalBankAccount, AdministrationBankAccountRequest BankAccount)
        {
            throw new NotImplementedException();
        }
    }
}

