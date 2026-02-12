namespace MpFinLar.API.Configuracoes;

public static class ConfiguracaoCors
{
    public static void ConfigurarCors(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration.GetSection("ConfiguracoesApp").Get<ConfiguracoesApp>();
        services.AddCors(options =>
            options.AddPolicy(name: "OrigensPermitidas", b =>
                b.WithOrigins(appSettings!.OrigensPermitidas)
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

    }
    public static void UsarCofiguracaoCors(this WebApplication app)
    {
        app.UseCors("OrigensPermitidas");
    }
}
