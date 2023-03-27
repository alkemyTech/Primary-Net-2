using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.repositories
{
    internal class FixedTermDepositRepository : GenericRepository<FixedTermDeposit> ,IFixedTermDepositRepository
    {
        public FixedTermDepositRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Task Add(FixedTermDeposit entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(FixedTermDeposit entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FixedTermDeposit>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<FixedTermDeposit> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(FixedTermDeposit entity)
        {
            throw new NotImplementedException();
        }
    }
}
