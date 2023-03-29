using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using PrimatesWallet.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Account> GetByUserId_FixedTerm(int userId)
        {
            var account = await base._dbContext.Accounts
                .Where(a => a.UserId == userId)
                .Include(b => b.FixedTermDeposit //join con fixedTermc
                    .OrderBy(c => c.Closing_Date)) //ordenamos en base de datos para mejor rendimiento
                .FirstOrDefaultAsync();

            return account;
        }

    }
}
