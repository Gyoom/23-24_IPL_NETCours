using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolApp.Repositories
{
    // interface générique pour les repositories, définis les opérations de base que l'on peux effectuer sur les collections de la db
    public interface IRepository<T> 
    {
        void Insert(T entity);
        void Delete(T entity);
        IList<T> SearchFor(Expression<Func<T, bool>> predicate);
        // sauve l'entité si l'élément n'existe pas déjà -> l'existence se base sur le prédicat
        bool Save(T entity,Expression<Func<T, bool>> predicate);
        IList<T> GetAll();
        T GetById(int id);
    }
}
