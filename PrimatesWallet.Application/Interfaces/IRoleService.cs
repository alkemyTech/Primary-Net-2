using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(int id);
    }
}
