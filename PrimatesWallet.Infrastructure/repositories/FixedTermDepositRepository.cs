using Microsoft.EntityFrameworkCore;
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

        private readonly ApplicationDbContext _dbContext;
        public FixedTermDepositRepository(ApplicationDbContext context) : base(context)
        {
            this._dbContext = context;
        }


        public async Task<FixedTermDeposit> GetFixedTermByIdAndUserId(int  userId , int fixedId)
        {
            var accountDeposits = await _dbContext.Accounts.Where(x => x.UserId == userId).Include(x => x.FixedTermDeposit).FirstOrDefaultAsync();
            var fixedTermDeposit = accountDeposits.FixedTermDeposit.FirstOrDefault(x => x.Id == fixedId);
            return fixedTermDeposit;  

        }
    }
}
