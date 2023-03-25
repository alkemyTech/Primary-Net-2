using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimatesWallet.Core.Models
{
    //TODO Quitar comentarios del miembro User y
    //estar atento al agregado de prop de navegación de otras entidades si se requieren
    public class Account
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("creationDate", TypeName = "DATETIME")]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Creation date is required")]
        public DateTime CreationDate { get; set; }
        [Column("money", TypeName = "DECIMAL")]
        [Range(0, Double.PositiveInfinity)]
        public decimal? Money { get; set; }
        [Column("isBlocked", TypeName = "BIT")]
        public bool IsBlocked { get; set; }
        [Column("user_id", TypeName = "INT")]
        public int UserId { get; set; }
        //public User User { get; set; }
    }
}
