using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Models;


namespace PrimatesWallet.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<FixedTermDeposit> FixedTermDeposits { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.Account_Id)
            .OnDelete(DeleteBehavior.Cascade);

            //pasamos el enum que contiene Admin, Regular a string
            //modelBuilder.Entity<Role>()
            //    .Property(x => x.Name)
            //    .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Type)
                .HasConversion<string>();
        }
    }
}
