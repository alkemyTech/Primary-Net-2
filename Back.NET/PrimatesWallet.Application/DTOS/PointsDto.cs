using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class PointsDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than zero.")]
        public int points { get; set; }
    }
}
