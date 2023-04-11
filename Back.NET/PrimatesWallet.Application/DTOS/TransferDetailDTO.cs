using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class TransferDetailDto
    {
        public string Concept { get; set; }
        public decimal Amount { get; set; }
        public string RecieverEmail { get; set; }
        public string RecieverFullname { get; set; }
    }
}
