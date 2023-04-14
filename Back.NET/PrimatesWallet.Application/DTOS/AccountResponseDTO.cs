using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class AccountResponseDTO
    {
        public int? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public decimal? Money { get; set; } = 0;
        public bool? IsBlocked { get; set; } = false;
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }
}
