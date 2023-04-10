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
        Task<IEnumerable<FixedTermDeposit>> GetAll(int page, int pageSize);
        Task<int> GetCount();
        Task<FixedTermDeposit> GetFixedTermDepositById(int userId , int fixedId);
    }
}
