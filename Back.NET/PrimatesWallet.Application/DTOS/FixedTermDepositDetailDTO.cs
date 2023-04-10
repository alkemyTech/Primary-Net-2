using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class FixedTermDepositDetailDto
    {
        public int Id { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Closing_Date { get; set; }
        public decimal Amount { get; set; }

    }
}
