using PrimatesWallet.Core.Models;


namespace PrimatesWallet.Application.Interfaces
{
    public interface IFixedTermDepositService
    {
        Task <FixedTermDeposit> GetFixedTermDepositById(int id );
        Task<IEnumerable<FixedTermDeposit>> GetByUser(int userId);
        Task<FixedTermDeposit> GetFixedTermDepositDetails(int id, int userId);
    }
}
