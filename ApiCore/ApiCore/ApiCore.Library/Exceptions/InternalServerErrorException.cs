using ApiCore.Library.Mensajes;
using System;
using System.Net;

namespace ApiCore.Library.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException() : base(ErrorMessages.InternalServerError, HttpStatusCode.InternalServerError) { }
        public InternalServerErrorException(string message) : base(message, HttpStatusCode.InternalServerError) {}
        public InternalServerErrorException(string message, Exception innerException) : base(message, HttpStatusCode.InternalServerError, innerException) { }
    }
}
