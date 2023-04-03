
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccountById(int id);
        Task<IEnumerable<Account>> GetAccountsList();
        Task<TransferDetailDto> Transfer(int remitentId, TransferDto transferDTO);
        Task<bool> ValidateAccount(int userId, int accountId);
        Task<bool> DepositToAccount(int Id, TopUpDto topUpDTO);
        Task<Account> UpdateAccountAdmin(int accountId, AccountUpdateDto accountUpdateDTO);
        Task<bool> Create(int userId);

    }
}
