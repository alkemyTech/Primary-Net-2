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

        public readonly IUnitOfWork unitOfWotk;

        public FixedTermDepositService (IUnitOfWork unitOfWotk)
        {
            this.unitOfWotk = unitOfWotk;
        }


        public async Task<FixedTermDeposit> GetFixedTermDepositById(int id)
        {
            try
            {
                var fixedTermDeposit = await unitOfWotk.FixedTermDeposits.GetById(id);
                return fixedTermDeposit;

            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<FixedTermDeposit>> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
