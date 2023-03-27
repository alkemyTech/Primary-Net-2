
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services;
using PrimatesWallet.Infrastructure.Seed;

using PrimatesWallet.Infrastructure.ServiceExtension;
using PrimatesWallet.Application.ServiceExtension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDIApplication(builder.Configuration);
// Add services to the container.
builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PopulateDataBase.Seed(app.Services);

app.Run();
