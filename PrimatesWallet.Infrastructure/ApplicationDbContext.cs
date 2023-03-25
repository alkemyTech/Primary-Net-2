using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //pasamos el enum que contiene Admin, Regular a string
            modelBuilder.Entity<Role>()
                .Property(x => x.Name)
                .HasConversion<string>();
        }
    }
}
