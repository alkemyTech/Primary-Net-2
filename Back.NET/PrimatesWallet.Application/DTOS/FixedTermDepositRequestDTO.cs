using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PrimatesWallet.Application.DTOS
{
    public class FixedTermDepositRequestDTO
    {
        [Range(100, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than 100.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The creation date field is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "The creation date field must be a valid date.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Creation_Date { get; set; }

        [Required(ErrorMessage = "The closing date field is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "The closing date field must be a valid date.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Closing_Date { get; set; }
    }
}
