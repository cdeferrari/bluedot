using Administracion.Integration.Model;
using RestSharp;
using System.Collections.Generic;


namespace Administracion.Integration.Contracts
{
    public interface IIntegration
    {
        void CheckStatusCode(IRestResponse response);
        void AddRestParameters(IRestRequest request, IEnumerable<RestParam> parameters);
        void AddRestBody(IRestRequest request, object body);
        IRestRequest BuildRestRequest(string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null);
    }
}
