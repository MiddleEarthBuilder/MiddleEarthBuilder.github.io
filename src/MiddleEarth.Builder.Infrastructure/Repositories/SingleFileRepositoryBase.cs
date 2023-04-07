using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Application;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

internal abstract class SingleFileRepositoryBase<TKey, TValue, TStoreValue> : IRepository<TKey, TValue> where TKey : notnull
{
    protected abstract string DataFilePath { get; }

    protected readonly BuilderContext Context;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SingleFileRepositoryBase<TKey, TValue, TStoreValue>> _logger;

    public SingleFileRepositoryBase(BuilderContext context, HttpClient httpClient, ILogger<SingleFileRepositoryBase<TKey, TValue, TStoreValue>> logger)
    {
        Context = context;
        _httpClient = httpClient;
        _logger = logger;
    }

    public IReadOnlyCollection<TValue> GetAll()
    {
        var task = GetAllAsync(CancellationToken.None).ConfigureAwait(false);
        return task.GetAwaiter().GetResult();
    }

    public async Task<IReadOnlyCollection<TValue>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        return dictionary.Values.ToArray();
    }

    public TValue GetOrCreate(TKey key)
    {
        var task = GetOrCreateAsync(key, CancellationToken.None).ConfigureAwait(false);
        return task.GetAwaiter().GetResult();
    }

    public async Task<TValue> GetOrCreateAsync(TKey key, CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        if (dictionary.TryGetValue(key, out var value))
            return value;

        value = CreateEmpty(key);
        dictionary[key] = value;
        return value;
    }

    public async Task UpdateAsync(TValue entity, CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        var key = GetKey(entity);
        dictionary[key] = entity;
    }

    public async Task DeleteAsync(TKey key, CancellationToken cancellationToken)
    {
        var dictionary = await GetDictionary(cancellationToken);
        dictionary.Remove(key);
    }

    protected abstract TKey GetKey(TValue entity);
    protected abstract TValue CreateEmpty(TKey key);

    private readonly SemaphoreSlim _semaphore = new(1);
    private IDictionary<TKey, TValue>? _dictionary;
    private async Task<IDictionary<TKey, TValue>> GetDictionary(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if (_dictionary != null)
                return _dictionary;

            var storeValues = await _httpClient.GetFromJsonAsync<IEnumerable<TStoreValue>>(DataFilePath, cancellationToken);
            _dictionary = storeValues == null ?
                new ConcurrentDictionary<TKey, TValue>() :
                new ConcurrentDictionary<TKey, TValue>(
                    storeValues.Select(storeValue =>
                    {
                        var value = Map(storeValue);
                        return new KeyValuePair<TKey, TValue>(GetKey(value), value);
                    }));

            return _dictionary;
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

    protected abstract TValue Map(TStoreValue raw);
}