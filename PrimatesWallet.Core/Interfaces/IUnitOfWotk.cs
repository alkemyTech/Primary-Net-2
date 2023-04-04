namespace PrimatesWallet.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITransactionRepository Transactions { get; }
        IFixedTermDepositRepository FixedTermDeposits { get; }
        ICatalogueRepository Catalogues { get; }
        IRoleRepository Roles { get; }
        IAccountRepository Accounts { get; }

        int Save();
    }
}
