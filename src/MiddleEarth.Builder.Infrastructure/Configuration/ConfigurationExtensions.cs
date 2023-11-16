using Microsoft.Extensions.DependencyInjection;
using MiddleEarth.Builder.Infrastructure.Files;

namespace MiddleEarth.Builder.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddContext(this IServiceCollection services) =>
        services
            .AddSingleton<Context>()
            .AddScoped<ContextExporter>()
            .AddScoped<ContextImporter>();
}