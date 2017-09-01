using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Library.Exceptions
{
    public abstract class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool NoLogApiInfra { get; set; }

        public BaseException() : base() { }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public BaseException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = statusCode;
        }
    }
}
