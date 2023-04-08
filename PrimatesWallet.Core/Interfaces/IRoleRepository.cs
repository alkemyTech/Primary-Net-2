using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<bool> AlreadyExistsName(string roleName);
        void UpdateRol(Role role);
    }
}
