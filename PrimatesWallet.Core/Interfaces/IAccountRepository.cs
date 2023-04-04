using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetByUserId_FixedTerm(int userId);
        Task<Account> Get_Transaccion(int Id);

        /// <summary>
        /// Obtiene el ID de la cuenta de un usuario especifico.
        /// </summary>
        /// <param name="userId">El ID del usuario para el que se desea obtener el ID de la cuenta.</param>
        /// <returns>El ID de la cuenta, si existe. De lo contrario, devuelve null.</returns>
        Task<int?> GetIdAccount(int userId);
        void UpdateAccountRepository(Account account);
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
    }
}
