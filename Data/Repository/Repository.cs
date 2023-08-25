using Data.Context;

namespace Unify.Data.Repositories;

public class Repository<TEntity> : BaseRepository<TEntity, int> where TEntity : class
{
    public Repository(BackOfficeDbContext context) : base (context) { }
}
