using Administracion.Integration.Model;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Administracion.Integration.Contracts
{
    public interface IAsync : IIntegration
    {
        void GetRestResponse(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null);

        void RestCallNoReturn(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null);
    }
}
