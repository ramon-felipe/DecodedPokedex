﻿namespace Decoded.Poke.Infrastructure.Repo;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class
{
}
