using ApiCore.Library.Mensajes;
using System;
using System.Net;

namespace ApiCore.Library.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException() : base(ErrorMessages.Forbidden, HttpStatusCode.Forbidden) { this.NoLogApiInfra = true; }
        public ForbiddenException(string message) : base(message, HttpStatusCode.Forbidden) { this.NoLogApiInfra = true; }
        public ForbiddenException(string message, Exception innerException) : base(message, HttpStatusCode.Forbidden, innerException) { this.NoLogApiInfra = true; }
    }
}
