using System.ComponentModel.DataAnnotations;

namespace PrimatesWallet.Application.DTOS
{
    public class TopUpDto
    {
        [Required]
        public decimal Money { get; set; }
        [Required]
        public string Concept { get; set; }
    }
}
