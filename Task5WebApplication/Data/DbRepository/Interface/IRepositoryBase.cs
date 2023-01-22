using System.Linq.Expressions;

namespace Task5WebApplication.Data.DbRepository.Interface;

public interface IRepositoryBase<T>
{
    public T FindByEmail(Expression<Func<T, bool>> expression);
    public T FindById(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void CreateAll(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
}