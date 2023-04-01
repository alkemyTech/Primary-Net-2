using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class TransferDTO
    {
        public string Concept { get; set; } = "Some";
        public decimal Amount { get; set; } 
        public string Email { get; set; }
        public TransactionType Type { get; set; }

    }
}
