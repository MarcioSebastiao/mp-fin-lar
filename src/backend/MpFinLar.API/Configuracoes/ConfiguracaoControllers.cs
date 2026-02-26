using System.Text.Json.Serialization;

namespace MpFinLar.API.Configuracoes;

public static class ConfiguracaoControllers
{
    public static void ConfigurarControllers(this IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Configura o serializer para NÃO incluir no JSON propriedades com valor null.
                    // Para reduzir o tamanho da resposta e evita enviar campos desnecessários na API.
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
    }
}