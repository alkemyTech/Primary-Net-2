using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;


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
    }
}
