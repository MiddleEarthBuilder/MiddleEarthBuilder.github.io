using MiddleEarth.Builder.Application;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiddleEarth.Builder.Infrastructure;

public abstract class RepositoryBase<TKey, TValue> : IRepository<TKey, TValue>
{
    protected abstract string DataDirectoryRoute { get; }

    private readonly Dictionary<TKey, TValue> _dictionary = new();
    private TKey[]? _index;

    private readonly HttpClient _httpClient;

    protected RepositoryBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<TValue>> Get(CancellationToken cancellationToken)
    {
        _index ??= await _httpClient.GetFromJsonAsync<TKey[]>($"{DataDirectoryRoute}/_index.json", cancellationToken);
        if (_index == null)
            return _dictionary.Values;

        foreach (var name in _index)
            await Get(name, cancellationToken);

        return _dictionary.Values;
    }

    public async Task<TValue?> Get(TKey name, CancellationToken cancellationToken)
    {
        if (_dictionary.TryGetValue(name, out var value))
            return value;

        try
        {
            var result = await _httpClient.GetFromJsonAsync<TValue>($"{DataDirectoryRoute}/{name}", cancellationToken);
            if (result == null)
                return default;

            _dictionary[name] = result;

            return result;
        }
        catch (JsonException ex)
        {
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