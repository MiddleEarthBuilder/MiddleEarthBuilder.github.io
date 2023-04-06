namespace MiddleEarth.Builder.Application;

public interface IRepository<TKey, TValue>
{
    IReadOnlyCollection<TValue> GetAll();
    Task<IReadOnlyCollection<TValue>> GetAllAsync(CancellationToken cancellationToken);
    TValue GetOrCreate(TKey key);
    Task<TValue> GetOrCreateAsync(TKey key, CancellationToken cancellationToken);
    Task UpdateAsync(TValue entity, CancellationToken cancellationToken);
    Task DeleteAsync(TKey key, CancellationToken cancellationToken);
}