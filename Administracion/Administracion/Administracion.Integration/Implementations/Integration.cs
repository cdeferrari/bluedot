using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using Administracion.Library.Exceptions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Integration.Implementations
{
    public abstract class Integration : IIntegration
    {
        public void CheckStatusCode(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.Created:
                    break;
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(response.Content);

                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException(response.Content);

                case HttpStatusCode.NotFound:
                    throw new NotFoundException();

                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException();

                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();

                default:
                    throw new InternalServerErrorException();
            }
        }

        public void AddRestParameters(IRestRequest request, IEnumerable<RestParam> parameters)
        {
            foreach (var p in parameters.Where(p => p != null))
            {
                if (p.Value != null)
                    request.AddParameter(p.Name, p.Value, RestMaps.ParamTypesMap[p.Type]);
            }
        }

        public void AddRestBody(IRestRequest request, object body)
        {
            request.AddJsonBody(body);
        }

        public IRestRequest BuildRestRequest(string url, RestMethod method, IEnumerable<RestParam> parameters = null, object body = null)
        {
            var request = new RestRequest(url, RestMaps.MethodsMap[method]);

            if (parameters != null)
                AddRestParameters(request, parameters);

            if (body != null)
                AddRestBody(request, body);

            return request;
        }




    }
}
