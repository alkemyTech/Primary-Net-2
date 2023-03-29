using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using PrimatesWallet.Infrastructure.repositories;

namespace PrimatesWallet.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Transaction>> GetAllByAccount(int id)
        {
            return await base._dbContext.Transactions
                 .Where(t => (t.Type == TransactionType.topup && t.Account_Id == id) //depositos
                     || (t.Type == TransactionType.payment && t.Account_Id == id) //transferencia realizadas
                     || (t.Type == TransactionType.payment && t.To_Account_Id == id)) //transferencias recibidas
                     .ToListAsync();
        }

    }
}
