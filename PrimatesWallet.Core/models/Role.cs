using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(7)]
        [Column("name",TypeName ="VARCHAR(15)")]
        public RoleName Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 10)]
        [Column("description",TypeName = "VARCHAR(500)")]
        public string Description { get; set; }
    }
}
