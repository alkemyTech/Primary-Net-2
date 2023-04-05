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
        public decimal? Money { get; set; } = 0;

        [Column("isBlocked", TypeName = "BIT")]
        public bool IsBlocked { get; set; } = false;

        [Column("user_id", TypeName = "INT")]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column( TypeName = "BIT" )]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<FixedTermDeposit>? FixedTermDeposit { get; set; }
    }
}
