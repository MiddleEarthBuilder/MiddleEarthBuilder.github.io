using Microsoft.Extensions.DependencyInjection;
using MiddleEarth.Builder.Infrastructure.Files;

namespace MiddleEarth.Builder.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddSingleton<BuilderContext>()
            .AddScoped<ContextExporter>()
            .AddScoped<ContextImporter>();
}