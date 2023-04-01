using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Infrastructure.repositories
{
    public class FixedTermDepositRepository : GenericRepository<FixedTermDeposit>, IFixedTermDepositRepository
    {

        public FixedTermDepositRepository(ApplicationDbContext context) : base(context)
        {

        }


        public async Task<FixedTermDeposit> GetFixedTermByIdAndUserId(int userId, int fixedId)
        {
            var accountDeposits = await _dbContext.Accounts.Where(x => x.UserId == userId).Include(x => x.FixedTermDeposit).FirstOrDefaultAsync();
            var fixedTermDeposit = accountDeposits.FixedTermDeposit.FirstOrDefault(x => x.Id == fixedId);
            return fixedTermDeposit;

        }

        public async Task<IEnumerable<FixedTermDeposit>> GetAll(int page, int pageSize)
        {
            //recuperamos en base de datos solo lo que necesitamos
            return await base._dbContext.FixedTermDeposits
                .Skip((page - 1) * pageSize) //saltamos lo anterior
                .Take(pageSize) //tomamos los 10 que necesitamos
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            //la cuenta se hace en base de datos para eficiencia
            return await base._dbContext.FixedTermDeposits.CountAsync();
        }
    }
}
