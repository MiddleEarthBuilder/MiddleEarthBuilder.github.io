using Microsoft.Extensions.DependencyInjection;
using MiddleEarth.Builder.Application;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddSingleton<IRepository<string, ArmyList>, ArmyListMultipleFilesRepository>();
}