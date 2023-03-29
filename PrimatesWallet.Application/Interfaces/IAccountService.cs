
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccountById(int id);
        Task<IEnumerable<Account>> GetAccountsList();
        Task<bool> DepositToAccount(int Id, TopUpDTO topUpDTO);
    }
}
