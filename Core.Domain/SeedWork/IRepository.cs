namespace Core.Domain.SeedWork;

public interface IRepository<T> where T : IAggregateRoot
{
    public IUnitOfWork UnitOfWork { get; }
}