using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Infra.Dados;

namespace MpFinLar.API.Configuracoes;

public static class ConfiguracaoContextoBanco
{
    public static void ConfigurarContextoBanco(this IServiceCollection services, IConfiguration configuration)
    {
        // Registra o DbContext Contexto no container de injeção de dependência
        services.AddDbContext<Contexto>(options =>
                // Configura o Entity Framework Core para utilizar o PostgreSQL usando o provedor Npgsql
                // Obtém a string de conexão "DefaultConnection" a partir do arquivo de configuração
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}