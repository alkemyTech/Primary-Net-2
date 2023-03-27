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
        Task <bool> CreateFixedTermDeposit( FixedTermDeposit fixedTermDeposit );
        Task<bool> UpdateFixedTermDeposit(FixedTermDeposit fixedTermDeposit );
        Task<bool> DeleteFixedTermDeposit(int fixedTermDepositId );
        Task <IEnumerable<FixedTermDeposit>> GetAllFixedTermDepositList();
        Task <FixedTermDeposit> GetFixedTermDepositById(int fixedTermDepositId );

    }
}
