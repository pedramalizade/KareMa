namespace KareMa.Infra.DataAccess.Repo.Ef.BaseRepository
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        protected IQueryable<TEntity> Queryable
            => _dbContext.Set<TEntity>();
    }
}
