using Administracion.Integration.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Integration.Contracts
{
    public interface ISync : IIntegration
    {
        byte[] RestCallRaw(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null);

        T RestCall<T>(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null) where T : new();

        IRestResponse GetRestResponse(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null);

        bool RestCallNoReturn(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null);

        JObject GetJsonRestResponse(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null);
    }
}
