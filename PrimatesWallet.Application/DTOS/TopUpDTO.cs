using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class TopUpDTO
    {
        [Required]
        public decimal Money { get; set; }
        [Required]
        public string Concept { get; set; }
        [Required]
        public TransactionType Type { get; set; }

    }
}
