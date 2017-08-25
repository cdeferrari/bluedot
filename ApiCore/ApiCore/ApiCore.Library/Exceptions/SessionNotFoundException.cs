using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Library.Exceptions
{
    public class SessionNotFoundException : BaseException
    {
        public SessionNotFoundException(string message) : base(message, HttpStatusCode.NotFound) { this.NoLogApiInfra = true; }
        public SessionNotFoundException(string message, Exception innerException) : base(message, HttpStatusCode.NotFound, innerException) { this.NoLogApiInfra = true; }
    }
}
