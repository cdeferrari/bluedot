using Administracion.Library.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Library.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base(ErrorMessages.BadRequest, HttpStatusCode.BadRequest) { this.NoLogApiInfra = true; }
        public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest) { this.NoLogApiInfra = true; }
        public BadRequestException(string message, Exception innerException) : base(message, HttpStatusCode.BadRequest, innerException) { this.NoLogApiInfra = true; }
    }
}
