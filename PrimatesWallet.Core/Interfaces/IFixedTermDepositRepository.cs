using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Interfaces
{
    public interface IFixedTermDepositRepository : IGenericRepository<FixedTermDeposit>
    {
        Task<FixedTermDeposit> GetFixedTermByIdAndUserId(int userId, int fixedId);
    }
}
