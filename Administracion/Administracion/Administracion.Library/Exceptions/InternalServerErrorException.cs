using Administracion.Library.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Library.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException() : base(ErrorMessages.InternalServerError, HttpStatusCode.InternalServerError) { }
        public InternalServerErrorException(string message) : base(message, HttpStatusCode.InternalServerError) { }
        public InternalServerErrorException(string message, Exception innerException) : base(message, HttpStatusCode.InternalServerError, innerException) { }
    }
}
