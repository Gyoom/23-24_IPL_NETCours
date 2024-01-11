using SchoolApp.Repositories;
using SchoolApp.Models;

namespace SchoolApp.UntitsOfWork
{
    // Implémente les getters pour accéder aux repository de la DB SQL
    // Fournis un context de fonction supplémentaire entre le repository et le context d'éxecution pour faire des tests unitaires
    class UnitOfWorkSQLServer : IUnitOfWork
    {
        private readonly SchoolContext _context;

        // Possèdent des repositories dédiés

        private SQLStudentRepository _studentRepository;

        private SQLSectionRepository _sectionRepository;

        // Ne possèdent pas de repository dédiés

        private SQLBaseRepository<Professor> _professorRepository;


        public UnitOfWorkSQLServer(SchoolContext context)
        {
            this._context = context;
            this._studentRepository = new SQLStudentRepository(context);
            this._sectionRepository = new SQLSectionRepository(context);
            this._professorRepository = new SQLBaseRepository<Professor>(context);

        }

        public IRepository<Student> StudentRepository
        {
            get { return this._studentRepository; }
        }

        public IRepository<Section> SectionRepository
        {
            get { return this._sectionRepository;  }
        }

        public IRepository<Professor> ProfessorRepository
        {
            get { return this._professorRepository; }
        }
    }
}
