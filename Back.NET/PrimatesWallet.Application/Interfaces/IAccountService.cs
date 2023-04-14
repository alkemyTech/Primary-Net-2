
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves a user account with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the account to retrieve.</param>
        Task<Account> GetAccountById(int id);


        /// <summary>
        /// Retrieves a list of all user accounts.
        /// </summary>
        /// <returns>An enumerable collection of all user accounts.</returns>
        Task<IEnumerable<Account>> GetAccountsList();


        /// <summary>
        /// This method receives a user identification extracted from the token and a DTO with the data of the recipient of the transfer.
        /// then verifies the existence of both accounts and finally updates the amounts of the same.
        /// </summary>
        /// <param name="remitentId">id of the remitent, extracted from the token</param>
        /// <param name="transferDTO">a DTO with the data of the receiver</param>
        /// <returns>a DTO with the confirmation of the transaction</returns>
        Task<TransferDetailDto> Transfer(int remitentId, TransferDto transferDTO);


        /// <summary>
        /// Validates that a user has access to a specific account.
        /// </summary>
        /// <param name="userId">The ID of the user to validate.</param>
        /// <param name="accountId">The ID of the account to validate.</param>
        /// <returns>True if the user has access to the account; otherwise, false.</returns>
        /// <exception cref="AppException">Thrown when the account does not exist.</exception>
        Task<bool> ValidateAccount(int userId, int accountId);


        /// <summary>
        /// Deposits a specified amount of money into a user's account.
        /// </summary>
        /// <param name="id">The ID of the user account to deposit money into.</param>
        /// <param name="topUpDTO">The top-up DTO object containing the information of the deposit.</param>
        /// <returns>A boolean value indicating whether the deposit was successful.</returns>
        Task<bool> DepositToAccount(int Id, TopUpDto topUpDTO);


        /// <summary>
        /// Updates an account's balance or block status.
        /// </summary>
        /// <param name="accountId">The ID of the account to update.</param>
        /// <param name="accountUpdateDTO">A DTO containing the updated account balance and block status.</param>
        /// <returns>The updated account object.</returns>
        /// <exception cref="AppException">Thrown when there is no account with the specified ID or the user does not have admin privileges.</exception>
        Task<Account> UpdateAccountAdmin(int accountId, AccountUpdateDto accountUpdateDTO);


        /// <summary>
        ///     This accountService method creates an account for a user if the user does not have one.
        /// </summary>
        /// <param name="userId">user id extraxted from a token.</param>
        /// <returns>if the account was created successfully, the method returns true</returns>
        /// <exception cref="AppException">If the user have an account, the method throw an error with status 400</exception>
        /// <exception cref="Exception">If there is an internal server error, the method catches it and throws an exception.</exception>
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


        /// <summary>
        /// Activates the specified account by ID.
        /// </summary>
        /// <param name="accountId">The ID of the account to activate.</param>
        /// <returns>A string message indicating that the account has been activated.</returns>
        Task<string> ActivateAccount(int accountId);


        /// <summary>
        /// Deletes the specified account by ID.
        /// </summary>
        /// <param name="accountId">The ID of the account to delete.</param>
        /// <param name="currentUser">The ID of the current user performing the action.</param>
        /// <returns>A string message indicating that the account has been deleted.</returns>
        Task<string> DeleteAccount(int accountId, int currentUser);
        //Task<AccountResponseDTO> BlockAccount(int accountId, AccountResponseDTO account);
        //Task<AccountResponseDTO> UnlockAccount(int accountId, AccountResponseDTO accountDto);
    }
}
