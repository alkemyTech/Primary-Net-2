
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

        /// <summary>
        /// Service to retrieve a paged list of accounts.
        /// </summary>
        /// <param name="page">The page number to return.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task that returns a list of accounts.</returns>
        /// <exception cref="AppException">Thrown if no accounts are found.</exception>
        Task<IEnumerable<AccountResponseDTO>> GetAccounts(int page, int pageSize);

        /// <summary>
        /// Obtains the total number of pages of accounts for a paged list.
        /// </summary>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task that returns the total number of pages.</returns>
        Task<int> TotalPageAccounts(int PageSize);
        Task<string> ActivateAccount(int accountId);
    }
}
