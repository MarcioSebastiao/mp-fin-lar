using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MpFinLar.API.Configuracoes;

public static class ConfiguracaoSwagger
{
    public static void ConfigurarSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => ConfigurarComentariosXmlSwagger(c));
    }

    private static void ConfigurarComentariosXmlSwagger(SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}
