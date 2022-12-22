namespace Domain.Seedwork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> SaveEntitiesAsync<T>(CancellationToken cancellationToken = default) where T : AggregateRoot;
}
