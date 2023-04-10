using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="roleCreationDto">The data to create the new role.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        Task<string> CreateRole(RoleCreationDto roleCreationDto);


        /// <summary>
        /// Retrieves a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>The role corresponding to the given ID.</returns>
        Task<Role> GetRoleById(int id);


        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>A collection of all roles.</returns>
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


        /// <summary>
        /// Activates a deleted role by its ID.
        /// </summary>
        /// <param name="roleId">The ID of the role to activate.</param>
        /// <returns>A message indicating that the role was successfully activated.</returns>
        Task<string> ActivateRole(int roleId);


        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="rolId">The ID of the role to update.</param>
        /// <param name="rolUpdateDTO">The new information for the role.</param>
        /// <param name="currentUser">The ID of the user making the request.</param>
        /// <returns>A message indicating the success of the update operation.</returns>
        Task<string> UpdateRol(int rolId, RolUpdateDto rolUpdateDTO, int currentUser);
    }
}
