using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateAccount(Account account);

        Task<IEnumerable<Account>> GetAllAccounts();

        Task<Account> GetAccountById(int accountId);

        Task<bool> UpdateAccount(Account account);

        Task<bool> DeleteAccount(int accountId);
    }
}
