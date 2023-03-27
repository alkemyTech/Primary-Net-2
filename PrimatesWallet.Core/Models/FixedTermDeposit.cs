using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimatesWallet.Core.Models
{


    public class FixedTermDeposit
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [Required]
        [Column("amount", TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Creation_Date { get; set; }
        
        [Required]
        public DateTime Closing_Date { get; set; }

    }
}