namespace PrimatesWallet.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository Transactions { get; }
        int Save();
    }
}
