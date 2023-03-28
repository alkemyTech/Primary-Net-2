namespace PrimatesWallet.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository UserRepository { get; }
        ITransactionRepository Transactions { get; }
        ICatalogueRepository Catalogues { get; }
        IRoleRepository Roles { get; }
        int Save();
    }
}
