using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;

namespace PrimatesWallet.Infrastructure.repositories
{
    public class FixedTermDepositRepository : GenericRepository<FixedTermDeposit>, IFixedTermDepositRepository
    {

        public FixedTermDepositRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override async Task<IEnumerable<FixedTermDeposit>> GetAll()
        {
            return await _dbContext.FixedTermDeposits.Where(x => !x.IsDeleted).ToListAsync();
        }

        public override async Task<FixedTermDeposit> GetById(int id)
        {
            return await _dbContext.FixedTermDeposits.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public override async Task<FixedTermDeposit> GetByIdDeleted(int id)
        {
            return await _dbContext.FixedTermDeposits.Where(x => x.Id == id && x.IsDeleted).FirstOrDefaultAsync();
        }


        public async Task<FixedTermDeposit> GetFixedTermDepositById(int userId, int fixedId)
        {
            // Selecciona un Plazo fijo espescífico para el usuario que lo requiere
            var accountDeposits = await _dbContext.Accounts.Where(x => x.UserId == userId ).Include(x => x.FixedTermDeposit).FirstOrDefaultAsync();
            if (accountDeposits == null) throw new AppException("No deposits in this account", HttpStatusCode.NoContent);
            var fixedTermDeposit = accountDeposits.FixedTermDeposit.FirstOrDefault(x => x.Id == fixedId && x.IsDeleted == false);
            return fixedTermDeposit;

        }

        public async Task<IEnumerable<FixedTermDeposit>> GetAll(int page, int pageSize)
        {
            //recuperamos en base de datos solo lo que necesitamos
            return await base._dbContext.FixedTermDeposits
                .Where( x => x.IsDeleted == false)
                .Skip((page - 1) * pageSize) //saltamos lo anterior
                .Take(pageSize) //tomamos los 10 que necesitamos
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            //la cuenta se hace en base de datos para eficiencia
            return await base._dbContext.FixedTermDeposits.Where(x => x.IsDeleted == false).CountAsync();
        }

        public async Task<IEnumerable<FixedTermDeposit>> GetClosedFixedTermDeposits()
        {
            var today = DateTime.Now.Date;

            return await base._dbContext.FixedTermDeposits.Where(f => f.Closing_Date.Date == today).Include(f => f.Account).ToListAsync();
        }
    }
}
