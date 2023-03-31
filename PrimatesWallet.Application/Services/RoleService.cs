
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Services
{
    public class RoleService : IRoleService
    {
        public readonly IUnitOfWork unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method returns the existing the roles 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Role>> GetRoles()
        {
            try
            {
                var roles = await unitOfWork.Roles.GetAll();
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This method searches for a specific ID and returns the requested role. 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleById(int roleId)
        {
            try
            {
                return await unitOfWork.Roles.GetById(roleId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
