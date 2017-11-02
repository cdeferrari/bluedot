using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Integration.Implementations
{
    public class Synchronic : Integration, ISync
    {
        public byte[] RestCallRaw(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null)
        {
            var response = GetRestResponse(urlBase, url, method, parameters, body);
            CheckStatusCode(response);
            return response.RawBytes;
        }



        public T RestCall<T>(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null) where T : new()
        {
            try
            {
                var client = new RestClient(urlBase);
                var request = BuildRestRequest(url, method, parameters, body);
                var response = client.Execute(request);
                CheckStatusCode(response);

                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IRestResponse GetRestResponse(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null)
        {
            var client = new RestClient(urlBase);
            var request = BuildRestRequest(url, method, parameters, body);
            return client.Execute(request);
        }

        public bool RestCallNoReturn(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null)
        {
            var response = GetRestResponse(urlBase, url, method, parameters, body);
            return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created;
        }

        public JObject GetJsonRestResponse(string urlBase, string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null)
        {
            try
            {
                var client = new RestClient(urlBase);
                var request = BuildRestRequest(url, method, parameters, body);
                var response = client.Execute(request);
                CheckStatusCode(response);

                return JObject.Parse(response.Content);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
