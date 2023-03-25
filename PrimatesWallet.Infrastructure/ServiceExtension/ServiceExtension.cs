

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimatesWallet.Core.Interfaces;
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

            services.AddScoped<IUnitOfWotk, UnitOfWork>();
            //EJ: services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
