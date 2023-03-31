using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        //implementacion de los metodos de la interfaz si se necesitan metodos distintos a los genericos
        public UserRepository(ApplicationDbContext context) : base(context) 
        {
        }

        //Metodo para obtener un usuario vía email y hacer un inner join con la tabla Role.
        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbContext.Users.Where(u => u.Email == email).Include(u => u.Role).FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetAccountByUserEmail(string email)
        {
            try
            {
                var user = await base._dbContext.Users.Where(x => x.Email == email).Include(x => x.Account).FirstOrDefaultAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool>IsRegistered(string email)
        {     
            var user = await base._dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null) return false;
            return true;
        }

    }
}
