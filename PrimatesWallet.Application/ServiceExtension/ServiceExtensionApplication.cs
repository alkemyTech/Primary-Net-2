﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services;


namespace PrimatesWallet.Application.ServiceExtension
{
    public static class ServiceExtensionApplication
    {
        public static IServiceCollection AddDIApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserService,UserService> ();
            services.AddScoped<ICatalogueService, CatalogueService>();
            services.AddScoped<IFixedTermDepositService, FixedTermDepositService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
