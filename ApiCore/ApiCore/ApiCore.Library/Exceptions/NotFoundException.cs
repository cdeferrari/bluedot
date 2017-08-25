using ApiCore.Library.Mensajes;
using System;
using System.Net;

namespace ApiCore.Library.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base(ErrorMessages.NotFound, HttpStatusCode.NotFound) { this.NoLogApiInfra = true; }
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound) { this.NoLogApiInfra = true; }
        public NotFoundException(string message, Exception innerException) : base(message, HttpStatusCode.NotFound, innerException) { this.NoLogApiInfra = true; }
    }
}
