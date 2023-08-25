using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Data.Context;
using Models.Common;
using Data.Repositories;

namespace Unify.Data.Repositories
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected BackOfficeDbContext context { get; set; }

        public BaseRepository(BackOfficeDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Queryable()
            => context.Set<TEntity>();

        public async Task<List<TEntity>> GetAll(bool ignoreAutoIncludes = false)
        {
            var query = Queryable();
            if (ignoreAutoIncludes) query = query.IgnoreAutoIncludes();

            return await query.ToListAsync();
        }

        public async Task<TEntity?> Find(TKey id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            return entity;
        }
        public async Task<TEntity?> Find(Expression<Func<TEntity, bool>> expression, bool ignoreAutoIncludes = false)
        {
            var query = Queryable();
            if (ignoreAutoIncludes) query = query.IgnoreAutoIncludes();

            return await query.Where(expression).FirstOrDefaultAsync();
        }
        public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> expression, bool ignoreAutoIncludes = false)
        {
            var query = Queryable();
            if (ignoreAutoIncludes) query = query.IgnoreAutoIncludes();

            return await query.Where(expression).ToListAsync();
        }

        public async Task Insert(TEntity entity, bool skipAudit = false)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync(skipAudit);
        }
        public async Task InsertAll(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                context.Set<TEntity>().Add(entity);
            }

            await context.SaveChangesAsync();
        }
        public async Task<TKey> InsertAndGetId(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            var id = entity.GetType().GetProperty("Id");
            return (TKey)id!.GetValue(entity)!;
        }

        public async Task Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes from the context without calling SaveChangesAsync().
        /// This is useful for change-tracking during Entity Auditing.
        /// </summary>
        public void RemoveFromContext(object child)
        {
            context.Entry(child).State = EntityState.Deleted;
        }

        public async Task Delete(TEntity entity, bool hardDelete = false)
        {
            Remove(entity, hardDelete);
            await context.SaveChangesAsync();
        }
        public async Task Delete(TKey id, bool hardDelete = false)
        {
            var entity = await Find(id);
            if (entity != null)
                Remove(entity, hardDelete);

            await context.SaveChangesAsync();
        }
        public async Task DeleteAll(List<TEntity> entities, bool hardDelete = false)
        {
            foreach (var entity in entities)
            {
                Remove(entity, hardDelete);
            }

            await context.SaveChangesAsync();
        }

        private void Remove(TEntity entity, bool hardDelete)
        {
            if (!hardDelete && typeof(TEntity).GetInterfaces().Contains(typeof(ISoftDelete)))
            {
                var prop = entity.GetType().GetProperty("IsDeleted");
                if (prop != null)
                {
                    prop.SetValue(entity, true);
                    context.Set<TEntity>().Update(entity);
                }
            }
            else
                context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity?> SingleFromSqlRaw(string query)
            => await context.Set<TEntity>()
                .FromSqlRaw(query)
                .FirstOrDefaultAsync();

        public async Task<List<TEntity>> ListFromSqlRaw(string query)
            => await context.Set<TEntity>()
                .FromSqlRaw(query)
                .ToListAsync();
    }
}
