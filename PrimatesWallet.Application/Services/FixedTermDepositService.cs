using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Services
{
    public class FixedTermDepositService : IFixedTermDepositService
    {

        public IUnitOfWotk _unitOfWotk;

        public FixedTermDepositService (IUnitOfWotk unitOfWotk)
        {
            _unitOfWotk = unitOfWotk;
        }

        public Task<bool> CreateFixedTermDeposit(FixedTermDeposit fixedTermDeposit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFixedTermDeposit(int fixedTermDepositId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FixedTermDeposit>> GetAllFixedTermDepositList()
        {
            throw new NotImplementedException();
        }

        public Task<FixedTermDeposit> GetFixedTermDepositById(int fixedTermDepositId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFixedTermDeposit(FixedTermDeposit fixedTermDeposit)
        {
            throw new NotImplementedException();
        }
    }
}
