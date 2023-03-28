using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Infrastructure.repositories;

namespace PrimatesWallet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFixedTermDepositRepository FixedTermDeposits;
        public ITransactionRepository Transactions { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository Roles { get; }
        public IAccountRepository Accounts { get; }

        //EJ: public IUserRepository Users { get; }

       

        public UnitOfWork(ApplicationDbContext dbContext, ITransactionRepository transactionRepository , IFixedTermDepositRepository fixedTermDepositRepository, IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _dbContext = dbContext;
            Transactions = transactionRepository;
            UserRepository = userRepository;
            FixedTermDeposits = fixedTermDepositRepository;
            Roles = roleRepository;
            Accounts = accountRepository;
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
