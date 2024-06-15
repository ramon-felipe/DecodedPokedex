using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Decoded.Poke.Infrastructure.Repo;

public interface IWriteRepository<in T>
{
    Result Add(T entity);
    Result Update(T entity);
    void Delete(int id);
    Result Save();
}