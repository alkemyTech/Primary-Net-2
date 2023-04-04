

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Infrastructure.repositories;
using PrimatesWallet.Infrastructure.Repositories;

namespace PrimatesWallet.Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IFixedTermDepositRepository, FixedTermDepositRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICatalogueRepository, CatalogueRepository>();


            return services;
        }
    }
}
