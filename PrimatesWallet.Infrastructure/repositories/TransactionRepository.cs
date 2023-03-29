

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

    }
}
