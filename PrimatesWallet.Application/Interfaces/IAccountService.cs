
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccountsList();
    }
}
