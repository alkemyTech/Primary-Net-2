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


        public override async Task<IEnumerable<Account>> GetAll()
        {
            return await _dbContext.Accounts.Where(x => !x.IsDeleted).ToListAsync();
        }


        public override async Task<Account> GetById(int id)
        {
            return await _dbContext.Accounts.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
        }


        public override async Task<Account> GetByIdDeleted(int id)
        {
            return await _dbContext.Accounts.Where(x => x.Id == id && x.IsDeleted).FirstOrDefaultAsync();
        }


        public async Task<Account> GetByUserId_FixedTerm(int userId)
        {
            var account = await base._dbContext.Accounts
                .Where(a => a.UserId == userId && a.IsDeleted == false)
                .Include(b => b.FixedTermDeposit //join con fixedTermc
                .OrderBy(c => c.Closing_Date)) //ordenamos en base de datos para mejor rendimiento
                .FirstOrDefaultAsync();

            return account;
        }


        public async Task<Account> Get_Transaccion(int Id)
        {
            var transaction = await base._dbContext.Accounts
                .Where(a => a.UserId == Id && a.IsDeleted == false)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync();

            return transaction;
        }


        public async Task<int?> GetIdAccount(int userId)
        {
            return await base._dbContext.Accounts
                .Where(a => a.UserId == userId && a.IsDeleted == false)
                .Select(x => x.Id) //retornamos de la db solo el id porque no se necesita el objeto completo
                .FirstOrDefaultAsync();
        }


        public async Task<bool> CheckAccountByUserId(int userId)
        {
            var existingAccount = await base._dbContext.Accounts.Where(a => a.UserId == userId && a.IsDeleted == false).FirstOrDefaultAsync();
            return (existingAccount == null) ? false : true;
        }


        public void UpdateAccountRepository(Account account)
        {
            _dbContext.Accounts.Update(account);
        }


        public async Task<IEnumerable<Account>> GetAll(int page, int pageSize)
        {
            return await base._dbContext.Accounts
                .Where(a => a.IsDeleted == false)
                .Include(x => x.User)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<int> GetCount()
        {
            return await base._dbContext.Accounts.Where(a => a.IsDeleted == false).CountAsync();
        }


        public void DeleteAccount(Account account)
        {
            _dbContext.Accounts.Remove(account);
        }

    }
}
