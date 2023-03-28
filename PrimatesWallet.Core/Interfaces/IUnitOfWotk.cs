namespace PrimatesWallet.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IUserRepository UserRepository { get; }
        ITransactionRepository Transactions { get; }

        IRoleRepository Roles { get; }
        int Save();
    }
}
