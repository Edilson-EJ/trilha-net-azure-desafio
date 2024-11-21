using Microsoft.EntityFrameworkCore;
using TrilhaNetAzureDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Validando a Connection String
var connectionString = builder.Configuration.GetConnectionString("ConexaoPadrao");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'ConexaoPadrao' não configurada.");
}

// Add services to the container.
builder.Services.AddDbContext<RHContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configuração de Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
