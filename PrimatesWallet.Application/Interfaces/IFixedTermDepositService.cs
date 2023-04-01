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
        Task<FixedTermDepositDetailDTO> GetFixedTermDepositDetails(int id, int userId);
        Task<bool> DeleteFixedtermDeposit(int id);
    }
}
