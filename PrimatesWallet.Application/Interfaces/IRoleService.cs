using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrimatesWallet.Application.Interfaces
{
    public interface IRoleService
    {
        Task<string> CreateRole(RoleCreationDto roleCreationDto);
        Task<Role> GetRoleById(int id);
        Task<IEnumerable<Role>> GetRoles();

        /// <summary>
        /// Deletes a role by its ID.
        /// </summary>
        /// <remarks>
        /// Deletes the role with the provided ID. If the role is not found, the method will throw an exception with an HTTP status code 404 Not Found.
        /// </remarks>
        /// <param name="id">The ID of the role to be deleted.</param>
        /// <returns>Returns a boolean indicating the success or failure of the deletion operation.</returns>
        Task<bool> DeleteRol(int id);
    }
}
