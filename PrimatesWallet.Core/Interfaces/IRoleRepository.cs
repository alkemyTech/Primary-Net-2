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
        /// <summary>
        /// Checks if a role name already exists in the database.
        /// </summary>
        /// <param name="roleName">The name of the role to check.</param>
        /// <returns>A boolean indicating whether the role name already exists.</returns>
        Task<bool> AlreadyExistsName(string roleName);


        /// <summary>
        /// Updates the given role in the database.
        /// </summary>
        /// <param name="role">The role to update.</param>
        void UpdateRol(Role role);
    }
}
