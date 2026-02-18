using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Aplicacao.Transacoes;
using MpFinLar.API.Configuracoes;
using MpFinLar.API.Dominio.Interfaces;
using MpFinLar.API.Infra.Dados;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Configura o serializer para NÃO incluir no JSON propriedades com valor null.
                    // Para reduzir o tamanho da resposta e evita enviar campos desnecessários na API.
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

builder.Services.ConfigurarSwagger();
builder.Services.ConfigurarCors(builder.Configuration);
builder.Services.AddMemoryCache();

// Registra o DbContext Contexto no container de injeção de dependência
builder.Services.AddDbContext<Contexto>(options =>
        // Configura o Entity Framework Core para utilizar o PostgreSQL usando o provedor Npgsql
        // Obtém a string de conexão "DefaultConnection" a partir do arquivo de configuração
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPessoaAplicacao, PessoaAplicacao>();
builder.Services.AddScoped<ITransacaoAplicacao, TransacaoAplicacao>();
builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();

builder.Services.AddScoped<IRepositorioPessoa, RepositorioPessoa>();
builder.Services.AddScoped<ICategoriaAplicacao, CategoriaAplicacao>();
builder.Services.AddScoped<IRepositorioTransacao, RepositorioTransacao>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UsarCofiguracaoCors();

app.Run();
