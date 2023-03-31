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

        //Aca se define cualquier metodo de acceso a base de datos que no este en generic
        Task<User> GetByEmail(string email);
        Task<int> GetUserIdByEmail(string email);
        Task<bool> IsAdmin(User user);
        Task<bool> IsRegistered(string email);
    }
}
