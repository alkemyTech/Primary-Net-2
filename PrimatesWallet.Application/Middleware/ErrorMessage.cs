using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Middleware
{
    public class ErrorMessage
    {
        public string Error { get; set; }

        public ErrorMessage(string error) { Error = error; }
    }
}
