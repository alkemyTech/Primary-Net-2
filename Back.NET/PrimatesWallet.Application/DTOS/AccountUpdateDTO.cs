using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class AccountUpdateDto
    {
        [Column("money", TypeName = "DECIMAL")]
        [Range(0, Double.PositiveInfinity)]
        public decimal? Money { get; set; }

        [Column("isBlocked", TypeName = "BIT")]
        public bool IsBlocked { get; set; }
    }
}
