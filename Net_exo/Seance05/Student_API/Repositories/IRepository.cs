using System.Linq.Expressions;

namespace Student_API.Repositories
{
    public interface IRepository<T> 
    {
        void Insert(T entity);
        void Delete(T entity);
        IList<T> SearchFor(Expression<Func<T, bool>> predicate);
        bool Save(T entity,Expression<Func<T, bool>> predicate);
        IList<T> GetAll();
        T GetById(int id);
    }
}
