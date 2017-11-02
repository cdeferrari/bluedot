using Administracion.DomainModel;
using System.Collections.Generic;

namespace Administracion.Services.Contracts.Autentication
{
    public interface IAuthentication
    {
        Account Login(string userName, string password);
        List<Account> GetAll();

        void SaveAccessToken(string AccessToken);
    }
}
