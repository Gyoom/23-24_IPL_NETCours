using SchoolApp.Models;
using System.Linq.Expressions;

namespace SchoolApp.Repositories
{
    // Permet de stocker en local (temporaire) des données et effectuer les opértions de IRepository dessus (Sert à faire des test sans modifier les données de la vraie db)
    // Ne possède pas de BaseRepository
    class MemoryStudentRepository : MemoryBaseRepository<Student>
    {
        public MemoryStudentRepository() : base() { }

        public Student GetById(int id)
        {
            return this._collection.Where(s => s.StudentId == id).SingleOrDefault();
        }

        public IList<Student> GetStudentBySectionOrderByYearResult()
        {
            IList<Student> students = (from Student s in this._collection
                                       orderby s.Section.Name, s.YearResult descending
                                       select s).ToList();

            return students;
        }

    }
}
