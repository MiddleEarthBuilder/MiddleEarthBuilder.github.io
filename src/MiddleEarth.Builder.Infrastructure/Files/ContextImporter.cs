using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Infrastructure.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiddleEarth.Builder.Infrastructure.Files;

public class ContextImporter
{
    private readonly HttpClient _client;
    private readonly ILogger<ContextImporter> _logger;

    public ContextImporter(HttpClient client, ILogger<ContextImporter> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<ContextRaw> GetDefault(CancellationToken cancellationToken) =>
        await _client.GetFromJsonAsync<ContextRaw>("/data/default.json", cancellationToken) ??
        throw new Exception("Failed to fetch context.");

    public async Task<ContextRaw> GetFromStream(Stream stream, CancellationToken cancellationToken) =>
        await JsonSerializer.DeserializeAsync<ContextRaw>(stream, cancellationToken: cancellationToken) ??
        throw new Exception("Failed to deserialize data from stream.");
}