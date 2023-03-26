using PrimatesWallet.Core.Models;
using PrimatesWallet.Infrastructure.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimatesWallet.Core.Interfaces;

namespace PrimatesWallet.Infrastructure.Repositories
{
    internal class FixedTermDepositRepository : GenericRepository<Catalogue>, IFixedTermDeposit
    {
        public FixedTermDepositRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
