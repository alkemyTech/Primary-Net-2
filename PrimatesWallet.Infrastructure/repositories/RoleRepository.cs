using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Infrastructure.repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context) 
        {

        }
    }
}
