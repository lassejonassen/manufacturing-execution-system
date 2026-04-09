namespace SharedKernel;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    TEntity Add(TEntity entity);
    void Delete(TEntity entity);
}
