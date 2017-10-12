using System.Threading.Tasks;

namespace ims.DataAccess.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, object domainModel)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Save();

        Task SaveAsync();

        void AutoDetectChangesEnabled();
        void ValidateOnSaveEnabled();

        void AutoDetectChangesDisable();
        void ValidateOnSaveDisable();
    }
}