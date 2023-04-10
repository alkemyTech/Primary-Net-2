using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
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

            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);



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

            ///<summary>
            ///This code sets up and configures Hangfire, a popular .NET library for background job processing,
            ///by adding it as a service to the dependency injection container, specifying a storage provider, and starting the Hangfire server.
            /// </summary>

            services.AddHangfire(config =>
            {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            });


            });

            services.AddHangfireServer();

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore 
            );


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
