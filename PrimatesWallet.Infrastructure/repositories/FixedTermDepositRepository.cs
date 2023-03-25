using PrimatesWallet.Core.Models;
using PrimatesWallet.Infrastructure.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Repositories
{
    internal class FixedTermDepositRepository : GenericRepository<Catalogue>
    {
        public FixedTermDepositRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
