using Microsoft.Extensions.DependencyInjection;
using MiddleEarth.Builder.Application.Files;

namespace MiddleEarth.Builder.Application.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddContext(this IServiceCollection services) =>
        services
            .AddSingleton<Context>()
            .AddScoped<ContextExporter>()
            .AddScoped<ContextImporter>();
}