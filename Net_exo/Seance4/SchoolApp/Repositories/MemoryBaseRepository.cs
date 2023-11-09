using Microsoft.EntityFrameworkCore;
using SchoolApp.Models;
using System.Linq.Expressions;

namespace SchoolApp.Repositories
{
    
    public class MemoryBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected List<TEntity> _collection;

        public MemoryBaseRepository()
        {
            this._collection = new List<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            this._collection.Remove(entity);
        }

        public IList<TEntity> GetAll()
        {
            return this._collection;
        }

        public void Insert(TEntity entity)
        {
            this._collection.Add(entity);
        }

        public bool Save(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            TEntity s = SearchFor(predicate).SingleOrDefault();

            if (s == null)
            {
                Insert(entity);

            }
            return true;
        }

        public TEntity GetById(int id)
        {
            return _collection.Find(id);
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return this._collection.AsQueryable().Where(predicate).ToList();
        }
    }
}
