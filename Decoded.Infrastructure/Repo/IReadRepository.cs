using CSharpFunctionalExtensions;

namespace Decoded.Poke.Infrastructure.Repo;

public interface IReadRepository<out T>
{
    IMaybe<T> Get(int id);
    IMaybe<T> Get(Func<T, bool> func);
    IQueryable<T> GetAll();
}