using PrimatesWallet.Core.Interfaces;

namespace PrimatesWallet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IFixedTermDepositRepository FixedTermDeposits { get; }
        public ITransactionRepository Transactions { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository Roles { get; }
        public ICatalogueRepository Catalogues { get; }

        //EJ: public IUserRepository Users { get; }

       

        public UnitOfWork(ApplicationDbContext dbContext,
            ITransactionRepository transactionRepository,
            IFixedTermDepositRepository fixedTermDepositRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ICatalogueRepository catalogueRepository
            )
        {
            _dbContext = dbContext;
            Transactions = transactionRepository;
            UserRepository = userRepository;
            FixedTermDeposits = fixedTermDepositRepository;
            Roles = roleRepository;
            Catalogues = catalogueRepository;
            //EJ: Users = userRepository; 

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
