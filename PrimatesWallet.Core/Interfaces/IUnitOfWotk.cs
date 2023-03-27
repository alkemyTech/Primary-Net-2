namespace PrimatesWallet.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository UserRepository { get; }
        ITransactionRepository Transactions { get; }

        IRoleRepository Roles { get; }
        int Save();
    }
}
