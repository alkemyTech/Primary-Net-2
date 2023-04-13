using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Core.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        /// <summary>
        /// Retrieves an account for a given user and includes all associated fixed terms.
        /// </summary>
        /// <param name="userId">The ID of the user whose account with a fixed-term deposit to retrieve.</param>
        /// <returns> Account including all associated fixed-terms, or null if not found.</returns>
        Task<Account> GetByUserId_FixedTerm(int userId);


        /// <summary>
        /// Retrieves an account for a given user and includes all associated transactions.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An <see cref="Account"/> Account including all associated transactions.</returns>
        Task<Account> Get_Transaccion(int Id);


        /// <summary>
        /// Obtiene el ID de la cuenta de un usuario especifico.
        /// </summary>
        /// <param name="userId">El ID del usuario para el que se desea obtener el ID de la cuenta.</param>
        /// <returns>El ID de la cuenta, si existe. De lo contrario, devuelve null.</returns>
        Task<int?> GetIdAccount(int userId);


        /// <summary>
        /// Updates an account entity in the database.
        /// </summary>
        /// <param name="account">The account entity to be updated.</param>
        void UpdateAccountRepository(Account account);


        /// <summary>
        ///     Este m�todo valida si exista una cuenta con el id del usuario.
        /// </summary>
        /// <param name="userId">
        ///     El valor de userId es extraido del token de autenticaci�n.
        /// </param>
        Task<bool> CheckAccountByUserId(int userId);


        /// <summary>
        /// Returns a paginated list of accounts.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of accounts.</returns>
        Task<IEnumerable<Account>> GetAll(int page, int pageSize);


        /// <summary>
        /// Returns the total number of accounts in the database.
        /// </summary>
        /// <returns>The total number of accounts.</returns>
        Task<int> GetCount();


        /// <summary>
        /// Deletes an account from the database.
        /// </summary>
        /// <param name="account">The account to be deleted.</param>
        void DeleteAccount(Account account);
    }
}
