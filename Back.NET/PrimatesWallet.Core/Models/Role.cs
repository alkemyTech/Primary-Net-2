using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimatesWallet.Core.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        /// <summary>
        /// Role id
        /// </summary>
        public int Id { get; set; }


        [Required]
        [MaxLength(7)]
        [Column("name", TypeName = "VARCHAR(15)")]
        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }


        [Required]
        [StringLength(255, MinimumLength = 10)]
        [Column("description", TypeName = "VARCHAR(500)")]
        /// <summary>
        /// Role description
        /// </summary>
        public string Description { get; set; }

        [Column("isDeleted", TypeName = "BIT")]
        public bool IsDeleted { get; set; } = false;
    }
}
