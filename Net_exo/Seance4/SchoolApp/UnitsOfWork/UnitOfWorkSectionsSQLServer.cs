using SchoolApp.Repositories;
using SchoolApp.Models;

namespace SchoolApp.UntitsOfWork
{
    class UnitOfWorkSectionsSQLServer : IUnitOfWorkSections
    {
        private readonly SchoolContext _context;

        private SQLSectionRepository _sectionsRepository;


        public UnitOfWorkSectionsSQLServer(SchoolContext context)
        {
            this._context = context;
            this._sectionsRepository = new SQLSectionRepository(context);

        }

        // object renvoyé est considéré comme un SQLBaseRepository 
        // (les methodes recrites ne sont pas prises en compte)
        // Donc on met le return à SQLStudentRepository même si ca reduit la flexibilité
        // --> Si pls Repo / UoW --> 1 methode get par repo
        public SQLSectionRepository SectionsRepository
        {
            get { return  this._sectionsRepository; }
        }
    }
}
