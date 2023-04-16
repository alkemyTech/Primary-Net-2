using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class UserResponseDto
    {
        public int? UserId { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public int? Points { get; set; } = 0;
        public string? Rol { get; set; }
        public int? AccountId { get; set; }
        public bool? IsAccountBlocked { get; set; }
        public decimal? Money { get; set; }
    }
}
