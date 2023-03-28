using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Seed.DataSeed
{
    public class RoleSeed : SeedData<Role>
    {
        protected override IEnumerable<Role> GetData()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = RoleName.Admin,
                    Description = "Rol para administrador de la aplicacion"
                },
                new Role()
                {
                    Name = RoleName.Regular,
                    Description = "Rol para Usuario con permisos limitados"
                }
            };
        }
    }
}
