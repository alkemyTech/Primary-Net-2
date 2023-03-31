
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccountById(int id);
        Task<IEnumerable<Account>> GetAccountsList();
        Task<TransferDetailDTO> Transfer(decimal amount, int remitentId, string recieverEmail, string concept = "Some");
        Task<bool> ValidateAccount(int userId, int accountId);
        Task<bool> DepositToAccount(int Id, TopUpDTO topUpDTO);
<<<<<<< HEAD
        Task<Account> UpdateAccountAdmin(int accountId, AccountUpdateDTO accountUpdateDTO);
=======
        Task<bool> Create(int userId);
>>>>>>> d0f78c7440ee290e388ad99a7b0df13846bb85ab
    }
}
