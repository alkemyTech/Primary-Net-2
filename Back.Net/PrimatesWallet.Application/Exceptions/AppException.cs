using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public AppException(string message, HttpStatusCode statusCode) :base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
