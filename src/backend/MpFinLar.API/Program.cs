using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Configuracoes;
using MpFinLar.API.Dominio.Interfaces;
using MpFinLar.API.Infra.Dados;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigurarSwagger();

// Registra o DbContext Contexto no container de injeção de dependência
builder.Services.AddDbContext<Contexto>(options =>
        // Configura o Entity Framework Core para utilizar o PostgreSQL usando o provedor Npgsql
        // Obtém a string de conexão "DefaultConnection" a partir do arquivo de configuração
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepositorioPessoa, RepositorioPessoa>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
