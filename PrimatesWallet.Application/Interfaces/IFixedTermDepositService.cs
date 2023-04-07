using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PrimatesWallet.Application.Interfaces
{
    public interface IFixedTermDepositService
    {
        Task <FixedTermDeposit> GetFixedTermDepositById(int id );
        Task<IEnumerable<FixedTermDeposit>> GetByUser(int userId);
        Task<FixedTermDepositDetailDto> GetFixedTermDepositDetails(int id, int userId);
        Task<bool> DeleteFixedtermDeposit(int id);
        //Task<IQueryable<FixedTermDeposit>> GetAllDepositsQueryable();
        Task<int> TotalPageDeposits(int pageSize);
        Task<IEnumerable<FixedTermDepositDetailDto>> GetDeposits(int page, int pageSize);


        /// <summary>
        /// Inserts a new fixed-term deposit for the specified user and subtracts the deposit amount from their account balance.
        /// </summary>
        /// <param name="id">The ID of the user making the deposit.</param>
        /// <param name="fixedTermDTO">The fixed-term deposit request object containing the deposit amount and creation/closing dates.</param>
        /// <returns>True if the deposit was successfully inserted, false otherwise.</returns>
        /// <exception cref="AppException">Thrown when there are insufficient funds or the deposit dates are invalid.</exception>
        Task<bool> Insert(int id, FixedTermDepositRequestDTO fixedTermDTO);
        Task LiquidateFixedTermDeposit();
        Task<string> ActivateFixedTermDeposit(int depositId);
        Task<bool> UpdateFixedTermDeposit(int id, FixedTermDepositRequestDTO fixedTermDeposit);
    }
}
