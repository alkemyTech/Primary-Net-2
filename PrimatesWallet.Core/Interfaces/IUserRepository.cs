using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //Aca se define cualquier metodo de acceso a base de datos que no este en generic
        Task<User> GetByEmail(string email);
    }
}
