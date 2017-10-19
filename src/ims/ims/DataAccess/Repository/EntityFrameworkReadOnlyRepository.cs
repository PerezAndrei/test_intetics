using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;

namespace ims.DataAccess.Repository
{
    public class EntityFrameworkReadOnlyRepository<TContext> : IReadOnlyRepository
        where TContext : DbContext
    {
        protected readonly TContext Context;

        public EntityFrameworkReadOnlyRepository(TContext context)
        {
            Context = context;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByDescending = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (orderByDescending != null)
            {
                query = orderByDescending(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="asNoTraking">ƒанные не кешируютс€ в EF. ѕараметр только дл€ того чтобы быстрее выбрать данные из таблицы</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTraking = false)
            where TEntity : class, IEntity
        {
            if (asNoTraking)
                return GetQueryable(null, orderBy, null, includeProperties, skip, take).AsNoTracking().ToList();
            else
                return GetQueryable(null, orderBy, null, includeProperties, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable(null, orderBy, null, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByDescending"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="asNoTraking">ƒанные не кешируютс€ в EF. ѕараметр только дл€ того чтобы быстрее выбрать данные из таблицы</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByDescending = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTraking = false)
            where TEntity : class, IEntity
        {
            if (asNoTraking)
                return GetQueryable(filter, orderBy, orderByDescending, includeProperties, skip, take).AsNoTracking().ToList();
            else
                return GetQueryable(filter, orderBy, orderByDescending, includeProperties, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable(filter, orderBy, null, includeProperties, skip, take).ToListAsync();
        }

        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
            where TEntity : class, IEntity
        {
            return GetQueryable(filter, null, null, includeProperties).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable(filter, null, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
            where TEntity : class, IEntity
        {
            return GetQueryable(filter, orderBy, null, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable(filter, orderBy, null, includeProperties).FirstOrDefaultAsync();
        }

        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter, includeProperties: includeProperties).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter, includeProperties: includeProperties).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter).AnyAsync();
        }
    }
}