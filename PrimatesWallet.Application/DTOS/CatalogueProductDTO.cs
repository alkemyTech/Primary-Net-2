using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class CatalogueProductDto
    {
        [Required]
        [Column("product_description", TypeName = "VARCHAR(500)")]
        public string ProductDescription { get; set; }

        [Required]
        [Column("image", TypeName = "VARCHAR(500)")]
        public string Image { get; set; }

        [Required]
        [Column("points")]
        public int Points { get; set; }
    }
}
