using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class RoleCreationDto
    {


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
    }
}
