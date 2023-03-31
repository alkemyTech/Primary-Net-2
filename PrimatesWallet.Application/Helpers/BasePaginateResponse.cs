using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Helpers
{
    public class BasePaginateResponse<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
        public int Page { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public int StatusCode { get; set; }
        public BasePaginateResponse()
        {
        }
    }
}
