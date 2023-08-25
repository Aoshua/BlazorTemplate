using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> Queryable();

        Task<List<TEntity>> GetAll(bool ignoreAutoIncludes = false);

        /// <summary>
        /// Find by the entity's primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> Find(TKey id);
        /// <summary>
        /// Find by a queried expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity?> Find(Expression<Func<TEntity, bool>> expression, bool ignoreAutoIncludes = false);

        /// <summary>
        /// Finds multiple records by a queried expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> expression, bool ignoreAutoIncludes = false);

        Task Insert(TEntity entity, bool skipAudit = false);
        Task InsertAll(List<TEntity> entities);
        Task<TKey> InsertAndGetId(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity, bool hardDelete = false);
        Task Delete(TKey id, bool hardDelete = false);
        Task DeleteAll(List<TEntity> entities, bool hardDelete = false);
    }
}
