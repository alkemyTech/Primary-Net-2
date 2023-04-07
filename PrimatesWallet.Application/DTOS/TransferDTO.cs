using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class TransferDto
    {
        public string Concept { get; set; } = "Some";
        public decimal Amount { get; set; } 
        public string Email { get; set; }
    }
}
