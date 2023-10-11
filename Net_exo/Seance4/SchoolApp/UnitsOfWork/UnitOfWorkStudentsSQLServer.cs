using SchoolApp.Repositories;
using SchoolApp.Models;

namespace SchoolApp.UntitsOfWork
{
    class UnitOfWorkStudentsSQLServer : IUnitOfWorkStudents
    {
        private readonly SchoolContext _context;

        private SQLStudentRepository _studentsRepository;


        public UnitOfWorkStudentsSQLServer(SchoolContext context)
        {
            this._context = context;
            this._studentsRepository = new SQLStudentRepository(context);

        }
        // object renvoyé est considéré comme un SQLBaseRepository 
        // (les methodes recrites ne sont pas prises en compte)
        // Donc on met le return à SQLStudentRepository même si ca reduit la flexibilité
        // --> Si pls Repo / UoW --> 1 methode get par repo
        public SQLStudentRepository StudentsRepository
        {
            get { return this._studentsRepository; }
        }
    }
}
