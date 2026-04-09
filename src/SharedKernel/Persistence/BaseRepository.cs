using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Persistence;

public abstract class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected DbContext DbContext => context;

    public TEntity Add(TEntity entity)
    {
        return context.Set<TEntity>().Add(entity).Entity;
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
}