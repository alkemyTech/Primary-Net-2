using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Models
{
    public class Catalogue
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("user_id")]
        //public User User { get; set; }

        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }

        [ForeignKey("account_id")]
        //public Account Account { get; set; }

        [Required]
        [Column("creation_date", TypeName = "DATETIME")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Column("closing_date", TypeName = "DATETIME")]
        public DateTime ClosingDate { get; set; }

        [Required]
        [Column("amount", TypeName = "DECIMAL")]
        public decimal Amount { get; set; }

    }
}
