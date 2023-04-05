
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;

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
            var roles = await unitOfWork.Roles.GetAll();
            return roles;  
        }

        /// <summary>
        /// This method searches for a specific ID and returns the requested role. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleById(int id)
        {
            var role =  await unitOfWork.Roles.GetById(id);
            return role;
        }

        /// <summary>
        /// This method creates a new Role validating if the name is already in the db and returns the confirmation if it is created. 
        /// </summary>
        /// <param name="roleCreationDto"></param>
        /// <returns></returns>
        public async Task<string> CreateRole(RoleCreationDto roleCreationDto)
        {
            var nameExists = await unitOfWork.Roles.AlreadyExistsName(roleCreationDto.Name);
            if (nameExists) throw new AppException("Role name already registered", HttpStatusCode.BadRequest);
            var newRole = new Role() { Description = roleCreationDto.Description, Name = roleCreationDto.Name };
            await unitOfWork.Roles.Add(newRole);
            unitOfWork.Save();
            var response = $"Role {roleCreationDto.Name} created";
            return response;

        }


        public async Task<bool> DeleteRol(int id)
        {
            var role = await unitOfWork.Roles.GetById(id) 
                ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            unitOfWork.Roles.Delete(role);
            var response = unitOfWork.Save();

            return response > 0;

        /// <summary>
        /// This method creates a new Role validating if the name is already in the db and returns the confirmation if it is created. 
        /// </summary>
        /// <param name="roleCreationDto"></param>
        /// <returns></returns>
        public async Task<string> CreateRole ( RoleCreationDto roleCreationDto)
        {
            var nameExists = await unitOfWork.Roles.AlreadyExistsName(roleCreationDto.Name);
            if (nameExists) throw new AppException("Role name already registered", HttpStatusCode.BadRequest);
            var newRole = new Role() { Description = roleCreationDto.Description, Name =  roleCreationDto.Name };
            await unitOfWork.Roles.Add(newRole);
            unitOfWork.Save();
            var response = $"Role {roleCreationDto.Name} created";
            return response;
        }

        public async Task<string> ActivateRole(int roleId)
        {
            var role = await unitOfWork.Roles.GetByIdDeleted(roleId);
            unitOfWork.Roles.Activate(role);
            unitOfWork.Save();
            return $"Role {roleId} activated";
        }
    }
}
