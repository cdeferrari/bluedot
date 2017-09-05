using Administracion.DomainModel;


namespace Administracion.Services.Contracts.Autentication
{
    public interface IAuthentication
    {
        Account Login(string userName, string password);

        void SaveAccessToken(string AccessToken);
    }
}
