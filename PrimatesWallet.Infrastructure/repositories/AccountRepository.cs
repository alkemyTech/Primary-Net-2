using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;

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

        public async Task<Account>Get_Transaccion(int Id)
        {
            return await base._dbContext.Accounts
                .Where(a => a.UserId == Id)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync();
        }

        public async Task<int?> GetIdAccount(int userId)
        {
            return await base._dbContext.Accounts
                .Where(a => a.UserId == userId)
                .Select(x => x.Id) //retornamos de la db solo el id porque no se necesita el objeto completo
                .FirstOrDefaultAsync();
        }

    }
}
