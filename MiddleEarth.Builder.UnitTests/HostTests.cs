using FluentAssertions;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiddleEarth.Builder.Infrastructure;
using MiddleEarth.Builder.Infrastructure.Configuration;
using MiddleEarth.Builder.Infrastructure.Files;
using MiddleEarth.Builder.WebAssembly.Http;

namespace MiddleEarth.Builder.UnitTests;

public class HostTests
{
    [Fact]
    public async Task Context_Should_Contain_Data()
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => services
            .Configure<ServiceOptions>(context.Configuration.GetSection(ServiceOptions.SectionKey))
            .AddScoped(_ =>
                new HttpClient(
                    new DefaultBrowserOptionsMessageHandler(new HttpClientHandler())
                    {
                        DefaultBrowserRequestCache = BrowserRequestCache.NoCache
                    }))
            .AddContext());

        var host = builder.Build();

        var importer = host.Services.GetRequiredService<ContextImporter>();
        var context = host.Services.GetRequiredService<Context>();
        await using var stream = File.OpenRead("../../../../src/MiddleEarth.Builder.WebAssembly/wwwroot/data/default.json");
        var contextRaw = await importer.GetFromStream(stream, CancellationToken.None);
        await context.Load(contextRaw);

        context.GetArmyLists().Should().HaveCountGreaterThan(0);
        context.GetSpecialRules().Should().HaveCountGreaterThan(0);
    }
}