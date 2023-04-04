using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Infrastructure.repositories;

namespace PrimatesWallet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IFixedTermDepositRepository FixedTermDeposits { get; }
        public ITransactionRepository Transactions { get; }
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IAccountRepository Accounts { get; }
        public ICatalogueRepository Catalogues { get; }

        public UnitOfWork(ApplicationDbContext dbContext,
            ITransactionRepository transactionRepository,
            IFixedTermDepositRepository fixedTermDepositRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ICatalogueRepository catalogueRepository,
            IAccountRepository accountRepository
            )
        {
            _dbContext = dbContext;
            Transactions = transactionRepository;
            Users = userRepository;
            FixedTermDeposits = fixedTermDepositRepository;
            Roles = roleRepository;
            Accounts = accountRepository;
            Catalogues = catalogueRepository;
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
