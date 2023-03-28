
﻿using PrimatesWallet.Application.Interfaces;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public async Task<Role> GetRoleById(int roleId)
        {
          try
          {
            return await unitOfWork.Roles.GetById(roleId);
          }
          catch(Exception ex)
          {
            return null;
          }
        }
    }
}
