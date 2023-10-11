using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Models;

namespace SchoolApp.Repositories
{
    internal class SQLStudentRepository : SQLBaseRepository<Student>
    {
        public SQLStudentRepository(SchoolContext context) : base(context) { }

        public new void Insert(Student entity)
        {
            List<Student> match = SearchFor(s => s.Firstname.Equals(entity.Firstname) && s.Name.Equals(entity.Name)).ToList();
            if (match.Count == 0)
            {
                _dbContext.Set<Student>().Add(entity);
                SaveChanges();
                Console.WriteLine("L'étudiant " + entity.Firstname + " " + entity.Name + " à été ajoutée");
            }
            else 
            {
                Console.WriteLine("L'étudiant " + entity.Firstname + " " + entity.Name + " existe déjà");
            }
        }
    }
}
