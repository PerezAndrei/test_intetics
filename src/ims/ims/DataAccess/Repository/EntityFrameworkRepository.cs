using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using ims.DataAccess.Repository;

namespace ims.DataAccess.Repository
{
    public class EntityFrameworkRepository<TContext> : EntityFrameworkReadOnlyRepository<TContext>, IRepository
        where TContext : DbContext
    {
        public EntityFrameworkRepository(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity
        {
            //entity.ModifiedOnServerUtc = DateTime.UtcNow;
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity
        {
            //entity.ModifiedOnServerUtc = DateTime.UtcNow;
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update<TEntity>(TEntity entity, object domainModel)
            where TEntity : class, IEntity
        {
            Context.Entry(entity).CurrentValues.SetValues(domainModel);
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return Context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        public void AutoDetectChangesEnabled()
        {
            Context.Configuration.AutoDetectChangesEnabled = true;
        }

        public void ValidateOnSaveEnabled()
        {
            Context.Configuration.ValidateOnSaveEnabled = true;
        }

        public void AutoDetectChangesDisable()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
        }

        public void ValidateOnSaveDisable()
        {
            Context.Configuration.ValidateOnSaveEnabled = false;
        }

        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }
    }
}