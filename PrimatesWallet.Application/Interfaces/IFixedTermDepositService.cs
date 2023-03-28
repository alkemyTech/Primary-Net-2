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

    }
}
