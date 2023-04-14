using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;


namespace PrimatesWallet.Infrastructure.repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        //implementacion de los metodos de la interfaz si se necesitan metodos distintos a los genericos
        public UserRepository(ApplicationDbContext context) : base(context) 
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.Where(x => !x.IsDeleted).ToListAsync();
        }

        public override async Task<User> GetById(int id)
        {
            return await _dbContext.Users.Where(x => x.UserId == id && !x.IsDeleted).Include(x => x.Account).Include(u => u.Role).FirstOrDefaultAsync();

        }

        public override async Task<User> GetByIdDeleted(int id)
        {
            return await _dbContext.Users.Where(x => x.UserId == id && x.IsDeleted).FirstOrDefaultAsync();
        }



        //Metodo para obtener un usuario vía email y hacer un inner join con la tabla Role.
        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbContext.Users.Where(u => u.Email == email && u.IsDeleted == false).Include(u => u.Role).FirstOrDefaultAsync();
            return user!;
        }

        public async Task<User> GetAccountByUserEmail(string email)
        {
            var user = await base._dbContext.Users.Where(x => x.Email == email && x.IsDeleted == false).Include(x => x.Account).FirstOrDefaultAsync();
            return user!;

        }
        public async Task<bool>IsRegistered(string email)
        {     
            var user = await base._dbContext.Users.Where(x => x.Email == email && x.IsDeleted == false).FirstOrDefaultAsync();
            if (user == null) return false;
            return true;
        }

        public bool IsAdmin (User user)
        {
            if (user == null) return false;
            if(user.Rol_Id == 1 ) return true;
            return false;
        }


        public async Task<int> GetUserIdByEmail(string email)
        {
            var user = await base._dbContext.Users.Where(x => x.Email == email && x.IsDeleted == false).FirstOrDefaultAsync();
            if(user == null) return 0;
            return user.UserId;
        }
        
        public async Task<IEnumerable<User>> GetAll(int page, int pageSize )
        {
            //recuperamos en base de datos solo lo que necesitamos
            return await base._dbContext.Users
                .Where( x => x.IsDeleted == false)
                .Skip((page - 1) * pageSize) //saltamos lo anterior
                .Take(pageSize) //tomamos los 10 que necesitamos
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            //la cuenta se hace en base de datos para eficiencia
            return await base._dbContext.Users
                .Where(x => x.IsDeleted == false)
                .CountAsync();
        }
        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
