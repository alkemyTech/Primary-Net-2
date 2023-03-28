namespace PrimatesWallet.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository UserRepository { get; }
        ITransactionRepository Transactions { get; }
        IFixedTermDepositRepository FixedTermDeposits { get; }
        ICatalogueRepository Catalogues { get; }
        IRoleRepository Roles { get; }

        int Save();
    }
}
