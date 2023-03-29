using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimatesWallet.Core.Models
{
    public class Account
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("creationDate", TypeName = "DATETIME")]
        [Required(ErrorMessage = "Creation date is required")]
        public DateTime CreationDate { get; set; }

        [Column("money", TypeName = "DECIMAL")]
        [Range(0, Double.PositiveInfinity)]
        public decimal? Money { get; set; }

        [Column("isBlocked", TypeName = "BIT")]
        public bool IsBlocked { get; set; }

        [Column("user_id", TypeName = "INT")]
        public int UserId { get; set; }

        public User User { get; set; }

        [NotMapped]
        public ICollection<Transaction> Transactions { get; set; }
        //[NotMapped]
        public ICollection<FixedTermDeposit> FixedTermDeposit { get; set; }
    }
}
