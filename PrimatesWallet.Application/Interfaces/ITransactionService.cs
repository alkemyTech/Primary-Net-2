using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> CreateTransaction(Transaction transaction);

        Task<IEnumerable<Transaction>> GetAllTransactions();

        Task<Transaction> GetTransactionById(int transactionId);

        Task<bool> UpdateTransaction(Transaction transaction);

        Task<bool> DeleteTransaction(int transactionId);
    }
}
