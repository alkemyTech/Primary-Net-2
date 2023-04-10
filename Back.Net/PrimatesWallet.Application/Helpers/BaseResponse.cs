using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Helpers
{
    public class BaseResponse<T>
    {
        //clase para respuesta estandar en la api
        public string Message { get; set; }
        public T? Result { get; set; }
        public int StatusCode { get; set; }
        public BaseResponse(string message, T? Result, int statusCode)
        {
            this.Message = message;
            this.Result = Result;
            this.StatusCode = statusCode;
        }
    }
}
