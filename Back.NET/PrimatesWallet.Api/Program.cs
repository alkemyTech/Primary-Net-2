using Microsoft.OpenApi.Models;
using PrimatesWallet.Infrastructure.Seed;
using PrimatesWallet.Infrastructure.ServiceExtension;
using System.Reflection;
using PrimatesWallet.Application.ServiceExtension;
using System.Text.Json.Serialization;
using PrimatesWallet.Application.Middleware;
using Hangfire;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services;


var builder = WebApplication.CreateBuilder(args);


{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("localhost",
            builder => builder.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod());
    });

}

builder.Services.AddHttpContextAccessor();
builder.Services.AddDIApplication(builder.Configuration);
// Add services to the container.
builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1", new OpenApiInfo { Title = "Primates-Wallet", Description = "Demo Api for Primates Wallet", Version = "v1" });
    var fileName = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
    c.IncludeXmlComments(filePath);


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

var app = builder.Build();
app.UseCors("localhost");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                 "Primates-Wallet v1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


///<summary>
///This code sets up the Hangfire dashboard and schedules a recurring job to call the "LiquidateFixedTermDeposit" 
///method of an implementation of the "IFixedTermDepositService" interface every day at 8:15 AM local time.
/// </summary>
app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<IFixedTermDepositService>(service => service.LiquidateFixedTermDeposit(), "15 8 * * *", TimeZoneInfo.Local);


app.MapControllers();

PopulateDataBase.Seed(app.Services);

app.Run();
