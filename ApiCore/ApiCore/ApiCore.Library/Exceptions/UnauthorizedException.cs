using ApiCore.Library.Mensajes;
using System;
using System.Net;

namespace ApiCore.Library.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException() : base(ErrorMessages.Unauthorized, HttpStatusCode.Unauthorized) { this.NoLogApiInfra = true; }
        public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized) { this.NoLogApiInfra = true; }
        public UnauthorizedException(string message, Exception innerException) : base(message, HttpStatusCode.Unauthorized, innerException) { this.NoLogApiInfra = true; }
    }
}
