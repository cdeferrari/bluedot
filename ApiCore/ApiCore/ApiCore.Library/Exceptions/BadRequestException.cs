using ApiCore.Library.Mensajes;
using System;
using System.Net;

namespace ApiCore.Library.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base(ErrorMessages.BadRequest, HttpStatusCode.BadRequest) { this.NoLogApiInfra = true; }
        public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest) { this.NoLogApiInfra = true; }
        public BadRequestException(string message, Exception innerException) : base(message, HttpStatusCode.BadRequest, innerException) { this.NoLogApiInfra = true; }
    }
}
