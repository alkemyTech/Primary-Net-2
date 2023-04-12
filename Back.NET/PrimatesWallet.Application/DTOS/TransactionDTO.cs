using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class TransactionDto
    {
        public int? Id { get; set; }
        public decimal? Amount { get; set; }
        public string? Concept { get; set; }
        public string? Date { get; set; }
        public string? Type { get; set; }
        public int? Account_Id { get; set; }
        public int? To_Account_Id { get; set; }
    }
}
