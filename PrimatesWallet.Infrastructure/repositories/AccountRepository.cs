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

        public async Task<Account> Get_Transaccion(int Id)
        {
            var transaction = await base._dbContext.Accounts
                .Where(a => a.UserId == Id)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync();

            return transaction;
        }

        public async Task<int?> GetIdAccount(int userId)
        {
            return await base._dbContext.Accounts
                .Where(a => a.UserId == userId)
                .Select(x => x.Id) //retornamos de la db solo el id porque no se necesita el objeto completo
                .FirstOrDefaultAsync();
        }
        /// <summary>
        ///     Este m�todo valida si exista una cuenta con el id del usuario.
        /// </summary>
        /// <param name="userId">
        ///     El valor de userId es extraido del token de autenticaci�n.
        /// </param>
        public async Task<bool> CheckAccountByUserId(int userId)
        {
            var existingAccount =await base._dbContext.Accounts.Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return ( existingAccount == null ) ? false: true;
        }

        public void UpdateAccountRepository(Account account)
        {
            _dbContext.Accounts.Update(account);
        }

        public async Task<IEnumerable<Account>> GetAll(int page, int pageSize)
        {
            return await base._dbContext.Accounts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await base._dbContext.Accounts.CountAsync();
        }
    }
}
