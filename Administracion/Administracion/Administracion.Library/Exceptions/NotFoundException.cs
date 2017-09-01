using Administracion.Library.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Library.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base(ErrorMessages.NotFound, HttpStatusCode.NotFound) { this.NoLogApiInfra = true; }
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound) { this.NoLogApiInfra = true; }
        public NotFoundException(string message, Exception innerException) : base(message, HttpStatusCode.NotFound, innerException) { this.NoLogApiInfra = true; }
    }
}
