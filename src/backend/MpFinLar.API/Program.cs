using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Aplicacao.Transacoes;
using MpFinLar.API.Configuracoes;
using MpFinLar.API.Dominio.Interfaces;
using MpFinLar.API.Infra.Dados;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarControllers();
builder.Services.ConfigurarSwagger();
builder.Services.ConfigurarCors(builder.Configuration);
builder.Services.ConfigurarContextoBanco(builder.Configuration);
builder.Services.AddMemoryCache();


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
