namespace MiddleEarth.Builder.Application;

public interface IRepository<TKey, TValue>
{
    Task<IReadOnlyCollection<TValue>> Get(CancellationToken cancellationToken);
    Task<TValue?> Get(TKey key, CancellationToken cancellationToken);
    Task Update(TValue entity, CancellationToken cancellationToken);
    Task Delete(TKey key, CancellationToken cancellationToken);
}