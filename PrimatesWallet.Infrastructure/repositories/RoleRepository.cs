using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Infrastructure.repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context) 
        {

        }

        public async Task<bool> AlreadyExistsName(string roleName)
        {
            var exists = await _dbContext.Roles.Where(x => x.Name == roleName).FirstOrDefaultAsync();
            if (exists == null || roleName.ToLower() == exists.Name.ToLower()) return false;
            return true;
        }
    }
}
