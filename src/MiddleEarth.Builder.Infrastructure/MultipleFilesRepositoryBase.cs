using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Application;
using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiddleEarth.Builder.Infrastructure;

public abstract class MultipleFilesRepositoryBase<TKey, TValue> : IRepository<TKey, TValue>
{
    protected abstract string DataDirectoryPath { get; }

    private readonly IDictionary<TKey, TValue> _dictionary = new ConcurrentDictionary<TKey, TValue>();
    private TKey[]? _index;

    private readonly HttpClient _httpClient;
    private readonly ILogger<MultipleFilesRepositoryBase<TKey, TValue>> _logger;

    protected MultipleFilesRepositoryBase(HttpClient httpClient, ILogger<MultipleFilesRepositoryBase<TKey, TValue>> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<TValue>> Get(CancellationToken cancellationToken)
    {
        _index ??= await _httpClient.GetFromJsonAsync<TKey[]>($"{DataDirectoryPath}/_index.json", cancellationToken);
        if (_index == null)
            return _dictionary.Values.ToArray();

        foreach (var name in _index)
            await Get(name, cancellationToken);

        return _dictionary.Values.ToArray();
    }

    public async Task<TValue?> Get(TKey name, CancellationToken cancellationToken)
    {
        if (_dictionary.TryGetValue(name, out var value))
            return value;

        var path = $"{DataDirectoryPath}/{name}.json";
        try
        {
            var result = await _httpClient.GetFromJsonAsync<TValue>(path, cancellationToken);
            if (result == null)
                return default;

            _dictionary[name] = result;

            return result;
        }
        catch (JsonException exception)
        {
            _logger.LogError(exception, $"A JSON error occurred while loading \"{path}\".");
            return CreateNew(name);
        }
    }

    public Task Update(TValue entity, CancellationToken cancellationToken)
    {
        var key = GetKey(entity);
        _dictionary[key] = entity;
        return Task.CompletedTask;
    }

    public Task Delete(TKey key, CancellationToken cancellationToken)
    {
        _dictionary.Remove(key);
        return Task.CompletedTask;
    }

    protected abstract TValue CreateNew(TKey name);
    protected abstract TKey GetKey(TValue entity);
}