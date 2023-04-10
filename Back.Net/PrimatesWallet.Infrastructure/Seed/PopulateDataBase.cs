using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrimatesWallet.Infrastructure.Seed.DataSeed;


namespace PrimatesWallet.Infrastructure.Seed
{
    public static class PopulateDataBase
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate(); //migraciones actualizadas

                var seeders = new List<ISeedData>()
                {
                    new RoleSeed(),
                    new UserSeed(),
                    new CatalogueSeed(),
                    new AccountSeed(),
                    new TransactionSeed(),
                    new FixedTermDepositSeed(),
                };

                foreach (var seeder in seeders)
                {
                    seeder.Seed(context);
                }
            }
        }
    }
}
