using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Application;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

public abstract class MultipleFilesRepositoryBase<TKey, TValue, TStoreValue> : IRepository<TKey, TValue>
{
    protected abstract string DataDirectoryPath { get; }

    private readonly IDictionary<TKey, TValue> _dictionary = new ConcurrentDictionary<TKey, TValue>();
    private TKey[]? _index;

    protected readonly BuilderContext Context;
    private readonly HttpClient _httpClient;
    private readonly ILogger<MultipleFilesRepositoryBase<TKey, TValue, TStoreValue>> _logger;

    protected MultipleFilesRepositoryBase(BuilderContext context, HttpClient httpClient, ILogger<MultipleFilesRepositoryBase<TKey, TValue, TStoreValue>> logger)
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
        _index ??= await _httpClient.GetFromJsonAsync<TKey[]>($"{DataDirectoryPath}/_index.json", cancellationToken);
        if (_index == null)
            return _dictionary.Values.ToArray();

        foreach (var name in _index)
            await GetOrCreateAsync(name, cancellationToken);

        return _dictionary.Values.ToArray();
    }

    public TValue GetOrCreate(TKey key)
    {
        var task = GetOrCreateAsync(key, CancellationToken.None).ConfigureAwait(false);
        return task.GetAwaiter().GetResult();
    }

    public async Task<TValue> GetOrCreateAsync(TKey key, CancellationToken cancellationToken)
    {
        if (_dictionary.TryGetValue(key, out var value))
            return value;

        var path = $"{DataDirectoryPath}/{key}.json";
        try
        {
            var storeValue = await _httpClient.GetFromJsonAsync<TStoreValue>(path, cancellationToken);
            if (storeValue == null)
            {
                value = CreateEmpty(key);
                _dictionary[key] = value;
            }
            else
            {
                value = CreateEmpty(key);
                _dictionary[key] = value;
                Map(storeValue, value);
            }

            return value;
        }
        catch (JsonException exception)
        {
            _logger.LogWarning(exception, "A JSON error occurred while loading {path}.", path);
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("Data for path {path} was not found.", path);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogWarning(exception, "A HttpRequest error occurred while loading {path}.", path);
        }

        value = CreateEmpty(key);
        _dictionary[key] = value;

        return value;
    }

    public virtual Task UpdateAsync(TValue entity, CancellationToken cancellationToken)
    {
        var key = GetKey(entity);
        _dictionary[key] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TKey key, CancellationToken cancellationToken)
    {
        _dictionary.Remove(key);
        return Task.CompletedTask;
    }

    protected abstract TKey GetKey(TValue entity);
    protected abstract TValue CreateEmpty(TKey name);
    protected abstract void Map(TStoreValue storeValue, TValue value);
}