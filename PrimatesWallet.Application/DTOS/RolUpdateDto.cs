using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class RolUpdateDto
    {
        [Required]
        [MaxLength(15)]
        [Column("name", TypeName = "VARCHAR(15)")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
