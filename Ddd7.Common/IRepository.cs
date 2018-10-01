namespace Ddd7.Common
{
    public interface IRepository<T> where T : AggregateRoot
    {
        T GetOrCreate();
        T Save(T aggregateRoot);
    }
}