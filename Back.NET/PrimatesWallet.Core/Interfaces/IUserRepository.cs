using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetAccountByUserEmail(string email);
        Task<int> GetUserIdByEmail(string email);
        bool IsAdmin(User user);
        Task<bool> IsRegistered(string email);

        //Aca se define cualquier metodo de acceso a base de datos que no este en generic
        Task<User> GetByEmail(string email);

        /// <summary>
        /// Returns a paginated list of users.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of users.</returns>
        Task<IEnumerable<User>> GetAll(int page, int pageSize);

        /// <summary>
        /// Returns the total number of users in the database.
        /// </summary>
        /// <returns>The total number of users.</returns>
        Task<int> GetCount();
        void UpdateUser(User user);
    }
}
