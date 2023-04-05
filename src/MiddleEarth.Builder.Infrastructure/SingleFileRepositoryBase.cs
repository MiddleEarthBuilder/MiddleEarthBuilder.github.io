using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Application;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace MiddleEarth.Builder.Infrastructure;

internal abstract class SingleFileRepositoryBase<TKey, TValue> : IRepository<TKey, TValue> where TKey : notnull
{
    protected abstract string DataFilePath { get; }

    private readonly HttpClient _httpClient;
    private readonly ILogger<SingleFileRepositoryBase<TKey, TValue>> _logger;

    public SingleFileRepositoryBase(HttpClient httpClient, ILogger<SingleFileRepositoryBase<TKey, TValue>> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<TValue>> Get(CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        return dictionary.Values.ToArray();
    }

    public async Task<TValue?> Get(TKey key, CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        dictionary.TryGetValue(key, out var value);
        return value;
    }

    public async Task Update(TValue entity, CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        var key = GetKey(entity);
        dictionary[key] = entity;
    }

    public async Task Delete(TKey key, CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        dictionary.Remove(key);
    }

    protected abstract TKey GetKey(TValue entity);

    private readonly SemaphoreSlim _semaphore = new(1);
    private IDictionary<TKey, TValue>? _dictionary;
    private async Task<IDictionary<TKey, TValue>> GetDictionary(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return _dictionary ??= new ConcurrentDictionary<TKey, TValue>(
                await _httpClient.GetFromJsonAsync<IDictionary<TKey, TValue>>(DataFilePath, cancellationToken) ??
                new Dictionary<TKey, TValue>());
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"An error occurred while loading dictionary \"{DataFilePath}\".");
            return _dictionary ??= new ConcurrentDictionary<TKey, TValue>();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}