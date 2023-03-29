using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionById(int id);
    }
}
