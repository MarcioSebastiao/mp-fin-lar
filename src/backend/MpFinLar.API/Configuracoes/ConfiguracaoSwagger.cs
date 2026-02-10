namespace MpFinLar.API.Configuracoes;

public static class ConfiguracaoSwagger
{
    public static void ConfigurarSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
