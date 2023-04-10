using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimatesWallet.Core.Models
{


    public class FixedTermDeposit
    {
        
        /// <summary>
        /// FixedTermDeposit id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        
        /// <summary>
        /// Account who owns the FixedTermDeposit
        /// </summary>
        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }


        /// <summary>
        /// Amount of the FixedTermDeposit 
        /// </summary>
        [Required]
        [Column("amount", TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Creation date of the FixedTermDeposit 
        /// </summary>
        [Required]
        public DateTime Creation_Date { get; set; }


        /// <summary>
        /// Closing date of the FixedTermDeposit ,determines the interest rate of the operation
        /// </summary>
        [Required]
        public DateTime Closing_Date { get; set; }

        [Column("isDeleted", TypeName = "BIT")]
        public bool IsDeleted { get; set; } = false;

    }
}