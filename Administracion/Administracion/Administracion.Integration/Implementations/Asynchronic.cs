using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Integration.Implementations
{
    public class Asynchronic : Integration, IAsync
    {
        public void GetRestResponse(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null)
        {
            var client = new RestClient(urlBase);
            var request = BuildRestRequest(url, method, parameters, body);
            client.ExecuteAsync(request, (
                x => x = x
                //TODO implementar algún callback
                ));
        }

        public void RestCallNoReturn(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null)
        {
            GetRestResponse(urlBase, url, method, parameters, body);
        }
    }
}
