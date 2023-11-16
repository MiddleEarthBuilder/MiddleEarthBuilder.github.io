using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using MiddleEarth.Builder.Application;
using MiddleEarth.Builder.Application.Configuration;
using MiddleEarth.Builder.Application.Files;
using MiddleEarth.Builder.WebAssembly;
using MiddleEarth.Builder.WebAssembly.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .Configure<ServiceOptions>(builder.Configuration.GetSection(ServiceOptions.SectionKey))
    .AddScoped(_ =>
        new HttpClient(
            new DefaultBrowserOptionsMessageHandler(new HttpClientHandler())
            {
                DefaultBrowserRequestCache = BrowserRequestCache.NoCache
            })
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        })
    .AddContext()
    .AddBlazoredModal();

var host = builder.Build();

var importer = host.Services.GetRequiredService<ContextImporter>();
var context = host.Services.GetRequiredService<Context>();
var contextRaw = await importer.GetDefault(CancellationToken.None);
await context.Load(contextRaw);

await host.RunAsync();
