using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;

namespace PrimatesWallet.Application.Services
{
    public class AccountService : IAccountService
    {
        public readonly IUnitOfWork unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Account>> GetAccountsList()
        {
            try
            {
                return await unitOfWork.Accounts.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Account> GetAccountById(int id)
        {
            try
            {
                var account = await unitOfWork.Accounts.GetById(id);

                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
