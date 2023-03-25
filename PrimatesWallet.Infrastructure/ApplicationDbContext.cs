using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet <Catalogue> Catalogues { get; set; }

    }
}
