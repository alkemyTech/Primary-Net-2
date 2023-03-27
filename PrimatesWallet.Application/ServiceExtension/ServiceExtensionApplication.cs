using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Mapping;
using PrimatesWallet.Application.Services;


namespace PrimatesWallet.Application.ServiceExtension
{
    public static class ServiceExtensionApplication
    {
        public static IServiceCollection AddDIApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserService,UserService> ();

            new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProfile());
            }).CreateMapper();

            services.AddAutoMapper(typeof(ServiceExtensionApplication));



            return services;
        }
    }
}
