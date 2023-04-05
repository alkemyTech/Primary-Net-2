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
    }
}
