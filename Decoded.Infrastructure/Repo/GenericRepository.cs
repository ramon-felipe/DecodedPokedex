using CSharpFunctionalExtensions;
using Decoded.Poke.Domain;
using Microsoft.EntityFrameworkCore;

namespace Decoded.Poke.Infrastructure.Repo;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly PokeDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(PokeDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public Result Add(T entity)
    {
        _dbSet.Add(entity);
       
        return Result.Success();
    }

    public void Delete(int id)
    {
        var entityOrNothing = this.Get(id);

        if (entityOrNothing.HasNoValue)
            return;

        _dbSet.Remove(entityOrNothing.Value);
    }

    public IMaybe<T> Get(int id)
        => this.Get(_ => _.Id == id);

    public IMaybe<T> Get(Func<T, bool> func)
    {
        var entity = _dbSet.AsNoTracking().SingleOrDefault(func);

        return entity == null ? Maybe.None : Maybe.From(entity);
    }

    public IQueryable<T> GetAll()
        => _dbSet.AsNoTracking();

    public Result Save()
    {
        _context.SaveChanges();
        return Result.Success();
    }

    public Result Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

        return Result.Success();
    }
}
