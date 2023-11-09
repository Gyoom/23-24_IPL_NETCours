using Student_API.DTOModels;
using System.Linq.Expressions;

namespace Student_API.Repositories
{
    
    public class MemoryStudentRepository : IRepository<Student>
    {
        protected static List<Student> _collection = new List<Student>()
        {
            new Student { ID = 1, FirstName = "Paul", LastName = "Ochon", Birthdate = new DateTime(1950, 12, 1) },
            new Student { ID = 2, FirstName = "Daisy", LastName = "Drathey", Birthdate = new DateTime(1970, 12, 1) },
            new Student { ID = 3, FirstName = "Elie", LastName = "Coptaire", Birthdate = new DateTime(1980, 12, 1) }
        };


        public void Delete(Student entity)
        {
            _collection.Remove(entity);
        }

        public IList<Student> GetAll()
        {
            return _collection;
        }

        public void Insert(Student entity)
        {
            _collection.Add(entity);
        }

        public bool Save(Student entity, Expression<Func<Student, bool>> predicate)
        {
            Student s = SearchFor(predicate).SingleOrDefault();

            if (s == null)
            {
                Insert(entity);

            }
            return true;
        }

        public Student GetById(int id)
        {
            return _collection.Find(s => s.ID == id);
        }

        public IList<Student> SearchFor(Expression<Func<Student, bool>> predicate)
        {
            return _collection.AsQueryable().Where(predicate).ToList();
        }
    }
}
