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

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbContext.Users.Where(u => u.Email == email).Include(u => u.Role).FirstOrDefaultAsync();

            return user; 
        }

    }
}
