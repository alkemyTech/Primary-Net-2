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
<<<<<<< HEAD

        void UpdateAccountRepository(Account account);
=======
        Task<bool> CheckAccountByUserId(int userId);
>>>>>>> d0f78c7440ee290e388ad99a7b0df13846bb85ab
    }
}
