using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Mapping;
using PrimatesWallet.Application.Services;
using PrimatesWallet.Application.Services.Auth;
using System.Text;

namespace PrimatesWallet.Application.ServiceExtension
{
    public static class ServiceExtensionApplication
    {
        public static IServiceCollection AddDIApplication(this IServiceCollection services, IConfiguration configuration)
        {

            new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProfile());
            }).CreateMapper();

            //services.AddAutoMapper(typeof(ServiceExtensionApplication));
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IUserService,UserService> ();
            services.AddScoped<IRoleService, RoleService> ();
            services.AddScoped<ICatalogueService, CatalogueService>();
            services.AddScoped<IFixedTermDepositService, FixedTermDepositService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtJervice, JwtService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))

                    };
                });

            return services;
        }
    }
}
