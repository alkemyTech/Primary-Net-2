using PrimatesWallet.Core.Interfaces;

namespace PrimatesWallet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFixedTermDepositRepository FixedTermDeposits;
        public readonly ITransactionRepository Transactions { get; }
        public readonly IUserRepository UserRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext, ITransactionRepository transactionRepository , IFixedTermDepositRepository fixedTermDepositRepository, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            Transactions = transactionRepository;
            UserRepository = userRepository;
            FixedTermDeposits = fixedTermDepositRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
