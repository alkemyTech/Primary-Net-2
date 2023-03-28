using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.repositories
{
    public class FixedTermDepositRepository : GenericRepository<FixedTermDeposit> ,IFixedTermDepositRepository
    {
        public FixedTermDepositRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
