﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.ServiceExtension
{
    //public static class InfraestructureExtension
    //{
    //    public static IServiceCollection AddInfraestructureExtension(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        services.AddDbContext<PrimatesDBContext>(options =>
    //        {
    //            options.UseSqlServer(
    //                configuration.GetConnectionString("DefaultConnection")
    //                );

    //        });

    //        //services.AddScoped<IUnitOfWork, UnitOfWork>();
    //        //services.AddScoped<IProductRepository, ProductRepository>();

    //        return services;
    //    }
    //}
}